using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatBot.Models.Entities
{
    public class ChatbotQA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Makes Id an identity column
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

    }
}
