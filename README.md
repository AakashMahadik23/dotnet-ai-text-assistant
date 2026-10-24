# AI Text Assistant — .NET 10 & Ollama

A console-based AI Text Assistant built with C# and .NET 10 that uses a locally running Ollama language model to explain technical concepts, summarize text, and rewrite content.

This project explores AI integration in .NET using `IChatClient`, OllamaSharp, configuration management, asynchronous programming, cancellation tokens, and exception handling.

## Features

- **Explain Technical Concepts:** Explains topics in simple language and provides real-world examples where appropriate.
- **Summarize Text:** Condenses text while preserving key points and removing unnecessary details.
- **Rewrite Text:** Improves grammar, clarity, and readability while preserving the original meaning.
- **Local AI Inference:** Uses Ollama to run a language model locally, without requiring a paid cloud AI API for inference.
- **Configurable Model:** Configure the Ollama endpoint and model in `appsettings.json`.
- **Input Validation:** Rejects empty input and text exceeding 10,000 characters.
- **Asynchronous Requests:** Uses `async` and `await` to handle AI requests asynchronously.
- **Cancellation Support:** Passes a cancellation token to the AI request.
- **Error Handling:** Handles HTTP request errors and other exceptions.

## Technologies Used

- C#
- .NET 10
- Microsoft.Extensions.AI
- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Configuration.Json
- OllamaSharp
- Ollama

## Prerequisites

Before running the application, install:

1. [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
2. [Ollama](https://ollama.com/download)

You also need a compatible language model downloaded through Ollama. This project uses `phi3:mini` by default.

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/YOUR-GITHUB-USERNAME/dotnet-ai-text-assistant.git
cd dotnet-ai-text-assistant
```

Replace `YOUR-GITHUB-USERNAME` with your GitHub username.

### 2. Download the Ollama model

Open a terminal and run:

```bash
ollama pull phi3:mini
```

Verify that the model is available:

```bash
ollama list
```

### 3. Configure the application

Check that `appsettings.json` contains:

```json
{
  "Ollama": {
    "Endpoint": "http://localhost:11434/",
    "Model": "phi3:mini"
  }
}
```

You can change the model name to another model you've downloaded and want to use.

### 4. Restore dependencies

From the directory containing the `.csproj` file, run:

```bash
dotnet restore
```

### 5. Build the project

```bash
dotnet build
```

### 6. Run the application

Make sure the Ollama service is running, then execute:

```bash
dotnet run
```

## How to Use

When the application starts, it displays this menu:

```text
==================================
       AI TEXT ASSISTANT
==================================
Model: phi3:mini
Type 0 at the main menu to exit.

----------------------------------
1. Explain a technical concept
2. Summarize text
3. Rewrite text
0. Exit
----------------------------------
Choose an option:
```

### Option 1: Explain a Technical Concept

Select `1` and enter a topic.

Example input:

```text
Dependency Injection in ASP.NET Core
```

The model is instructed to explain the topic in simple language and use a real-world example where appropriate.

### Option 2: Summarize Text

Select `2` and enter a paragraph or longer text.

Example input:

```text
Dependency Injection is a design technique in which a class
receives its dependencies from an external source instead of
creating them itself. It improves testability and reduces
tight coupling between classes.
```

The model generates a shorter version that retains the key points.

### Option 3: Rewrite Text

Select `3` and enter text that needs improvement.

Example input:

```text
i am learning dotnet and building ai project
```

The model rewrites the text to improve grammar, clarity, and readability.

### Option 0: Exit

Select `0` to exit the application.

## Configuration

The application reads its settings from `appsettings.json`.

| Configuration key | Description | Default |
|---|---|---|
| `Ollama:Endpoint` | URL of the Ollama service | `http://localhost:11434/` |
| `Ollama:Model` | Name of the model used for generation | `phi3:mini` |

The configuration is loaded using `Microsoft.Extensions.Configuration` and its JSON configuration provider.

## Project Structure

```text
dotnet-ai-text-assistant/
├── Program.cs
├── AITextAssistant.csproj
├── appsettings.json
├── .gitignore
└── README.md
```

- **Program.cs:** Application entry point, menu, prompt construction, AI requests, and exception handling.
- **AITextAssistant.csproj:** Target framework and NuGet package references.
- **appsettings.json:** Ollama endpoint and model configuration.
- **.gitignore:** Excludes generated build output and local development files.
- **README.md:** Project documentation and setup instructions.

## Key Concepts Learned

- Integrating a local language model with a C# application.
- Using `IChatClient` as a common chat-client abstraction.
- Configuring an AI client with OllamaSharp.
- Loading application settings from JSON.
- Building prompts for different AI tasks.
- Using asynchronous programming with `Task` and `await`.
- Passing cancellation tokens to asynchronous operations.
- Validating user input.
- Handling exceptions and connection failures.
- Separating menu logic from text-processing logic.

## Troubleshooting

### Could not connect to Ollama

Make sure Ollama is installed and running. Verify that the configured endpoint is accessible and that the Ollama service is listening on the expected port.

### Model not found

Download the configured model:

```bash
ollama pull phi3:mini
```

If you choose a different model, update `appsettings.json` accordingly.

### Build errors

Check that the .NET 10 SDK is installed, then run:

```bash
dotnet --version
dotnet restore
dotnet build
```

### Configuration file not found

Ensure `appsettings.json` exists in the project directory and is copied to the application's output directory.

## Limitations

- This is a console application, not a web application.
- Ollama must be available on the machine running the application.
- AI response quality depends on the selected model.
- Model inference performance depends on available hardware and the model's resource requirements.
- The current implementation sends each operation as a single prompt and does not maintain conversation history.
- Cancellation is passed to the AI request, but graceful application shutdown can be improved.

## Future Improvements

- Add streaming responses so text appears as it is generated.
- Add conversation history.
- Separate prompt-building logic from the console interface.
- Add structured logging and improved cancellation handling.
- Add unit tests for input validation and prompt construction.
- Build a web interface using ASP.NET Core.
- Explore additional local models and AI integrations.

## License

This project is intended for learning and portfolio development. Add a license file if you want to specify how others may use, modify, and distribute your code.

---

**Author:** Aakash Mahadik

**Built with:** C#, .NET 10, Microsoft.Extensions.AI, OllamaSharp, and Ollama.