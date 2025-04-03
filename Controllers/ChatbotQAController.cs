using ChatBot.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatBot.Models;
using ChatBot.Data;
using System.Numerics;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Controllers
{
    public class ChatbotQAController : Controller
    {
        private readonly AppDbContext dbContext;
        private static List<(string Question, string Answer)> chatHistory = new List<(string, string)>();

        public ChatbotQAController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ChatbotQA viewModel)
        {
            var QuestionAnswer = new ChatbotQA
            {
                Question = viewModel.Question,
                Answer = viewModel.Answer
            };

            await dbContext.ChatbotQAs.AddAsync(QuestionAnswer);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "ChatbotQA");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var chatbotQAs = await dbContext.ChatbotQAs.ToListAsync();

            return View(chatbotQAs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var chatbotQA = await dbContext.ChatbotQAs.FindAsync(id);
            return View(chatbotQA);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ChatbotQA viewModel)
        {
            var chatbotQA = await dbContext.ChatbotQAs.FindAsync(viewModel.Id);

            if (chatbotQA != null)
            {
                chatbotQA.Question = viewModel.Question;
                chatbotQA.Answer = viewModel.Answer;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "ChatbotQA");

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var chatbotQA = await dbContext.ChatbotQAs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            dbContext.ChatbotQAs.Remove(chatbotQA);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "ChatbotQA");
        }

        [HttpPost]
        public async Task<IActionResult> AskQuestion([FromBody] QuestionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Question))
            {
                return Json(new { answer = "Please ask a valid question.", history = chatHistory });
            }

            var chatbotQAs = await dbContext.ChatbotQAs.ToListAsync();
            string bestMatch = "Sorry, I don't understand that question.";
            double maxMatchCount = 0.0; // Change from int to double

            var questionWords = request.Question.ToLower().Split(' ');
            

            foreach (var entry in chatbotQAs)
            {
                var entryWords = entry.Question.ToLower().Split(' ').ToHashSet();
                var commonWords = questionWords.Intersect(entryWords).Count();
                var totalWords = questionWords.Union(entryWords).Count();

                // Jaccard Similarity = (Intersection Count) / (Union Count)
                double similarity = (double)commonWords / totalWords;

                if (similarity > maxMatchCount)
                {
                    maxMatchCount = similarity;
                    bestMatch = entry.Answer;
                }
            }

            chatHistory.Add((request.Question, bestMatch));

            return Json(new { answer = bestMatch, history = chatHistory });
        }

        [HttpGet]
        public IActionResult ChatHistory()
        {
            return Json(chatHistory);
        }

    }
    public class QuestionRequest
    {
        public string Question { get; set; }
    }
}
