# ChatBot

This project is a simple chatbot built with **ASP.NET MVC** and **Bootstrap**. It allows users to interact with a chatbot that answers questions based on pre-defined question-answer pairs stored in a database.

The chatbot uses an **in-memory approach** to store the history of interactions and provides a chat window interface where users can ask questions and get answers. The bot’s name is randomly assigned once per session and remains consistent throughout the interaction.

## Features

- **Chatbot UI**: A simple and interactive chat window that can be toggled on/off.
- **Random Bot Name**: The bot’s name is randomly chosen once when the chat is initialized and remains the same throughout the session.
- **Question-Answer Pairs**: The bot can answer questions based on predefined question-answer pairs stored in the database.
- **History**: The chat history is stored and displayed, allowing users to see previous interactions.
- **Local Storage**: The chat history is saved to **localStorage**, so it persists across page reloads until the browser is closed.

## Prerequisites

Make sure you have the following installed on your local machine:

- **Visual Studio** or any other preferred IDE
- **.NET 6.0** (or later) SDK
- **SQL Server** (or a compatible database for storing questions/answers)

## Installation

### Clone the Repository

```bash
git clone https://github.com/nilayarangil/ChatBot.git
cd ChatBot
  ```

### **Set up the Database**

1. **Database Configuration**: Update your **AppDbContext** in the `ChatBot.Data` folder to point to your local SQL Server or use an in-memory database for quick testing. \

2. **Migrations**: If you are using a database, run the following commands to apply migrations:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
  ```

## **Usage**

1. **Interact with the Chatbot**: Once the application is running, navigate to the index page, and you will see a **Chatbot button** at the bottom right. Click on it to open the chat window. \

2. **Ask a Question**: You can type your question in the input field and click "Send" or press Enter. The chatbot will display its response based on predefined answers stored in the database. \

3. **Chat History**: The chatbot’s previous interactions will be displayed above the input field. The history is saved to **localStorage** to ensure that the chat persists across page reloads. \

4. **Adding/Editing Questions**: Administrators can add, edit, or delete predefined question-answer pairs from the database via the admin panel (`/ChatbotQA/List`). \



## **Bot Name Randomization**



* Upon opening the chat, a random bot name (e.g., "Alexa", "Siri", "Jarvis", etc.) is chosen and used for all responses in that session. \

* The bot name will remain the same until the session ends (e.g., page refresh or browser close). \



## **Code Structure**



* **Controllers**: \

    * `ChatbotQAController` handles adding, listing, editing, and deleting the question-answer pairs. \

    * `ChatbotController` manages the chat interactions, handling the logic for answering questions. \

* **Models**: \

    * `ChatbotQA` is the model used for storing question-answer pairs in the database. \

* **Views**: \

    * The chat UI is implemented using **Bootstrap** for styling and responsive design. \

    * The history of the chat and the ability to send messages are managed through JavaScript. \

* **JavaScript**: \

    * Handles the toggling of the chat window, message sending, and managing chat history in **localStorage**. \

    * Random bot name logic is implemented to ensure a consistent bot name for the session. \



## **Customizing the Chatbot**

You can easily customize the bot’s responses and the names it uses by modifying:



* The list of questions and answers in the database. \

* The **bot name array** in the JavaScript code. \



### **Add/Edit Questions:**

To add new questions or change existing ones, you can either use the **admin panel** (via the `ChatbotQAController`) or directly update the database with new entries.


### **Modify Chatbox UI:**

You can update the chatbox styles or modify the `index.cshtml` to fit your needs. The UI is designed using **Bootstrap**, so it’s responsive and can be adjusted according to your preference.
