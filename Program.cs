using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OllamaSharp;

// Load application configuration.
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string endpoint = configuration["Ollama:Endpoint"]
    ?? "http://localhost:11434";

string model = configuration["Ollama:Model"]
    ?? "phi3:mini";

// Set up cancellation support.
using var cancellationSource = new CancellationTokenSource();

Console.CancelKeyPress += (_, eventArgs) =>
{
    eventArgs.Cancel = true;
    cancellationSource.Cancel();
};

// Create the Ollama client and expose it through IChatClient.
using var ollamaClient = new OllamaApiClient(
    new Uri(endpoint),
    model);

IChatClient chatClient = ollamaClient;

Console.WriteLine("==================================");
Console.WriteLine("       AI TEXT ASSISTANT");
Console.WriteLine("==================================");
Console.WriteLine($"Model: {model}");
Console.WriteLine("Type 0 at the main menu to exit.");
Console.WriteLine();

bool exitRequested = false;

while (!exitRequested)
{
    ShowMenu();

    Console.Write("Choose an option: ");
    string? choice = Console.ReadLine()?.Trim();

    switch (choice)
    {
        case "1":
            await ProcessTextAsync(
                chatClient,
                "Explain the following topic in simple language. " +
                "Use a real-world example where appropriate.",
                "Enter a technical topic to explain:",
                cancellationSource.Token);
            break;

        case "2":
            await ProcessTextAsync(
                chatClient,
                "Summarize the following text. Preserve the key points " +
                "and remove unnecessary details.",
                "Enter the text to summarize:",
                cancellationSource.Token);
            break;

        case "3":
            await ProcessTextAsync(
                chatClient,
                "Rewrite the following text to improve grammar, clarity, " +
                "and readability while preserving its original meaning.",
                "Enter the text to rewrite:",
                cancellationSource.Token);
            break;

        case "0":
            exitRequested = true;
            Console.WriteLine("Thank you for using AI Text Assistant!");
            break;

        default:
            Console.WriteLine("Invalid choice. Select 0, 1, 2, or 3.");
            break;
    }

    Console.WriteLine();
}

static void ShowMenu()
{
    Console.WriteLine("----------------------------------");
    Console.WriteLine("1. Explain a technical concept");
    Console.WriteLine("2. Summarize text");
    Console.WriteLine("3. Rewrite text");
    Console.WriteLine("0. Exit");
    Console.WriteLine("----------------------------------");
}

static async Task ProcessTextAsync(
    IChatClient chatClient,
    string instruction,
    string inputPrompt,
    CancellationToken cancellationToken)
{
    Console.WriteLine();
    Console.WriteLine(inputPrompt);

    string? input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine("Input cannot be empty.");
        return;
    }

    if (input.Length > 10000)
    {
        Console.WriteLine(
            "Input is too long. Please enter fewer than 10,000 characters.");
        return;
    }

    string prompt = $"""
        {instruction}

        User input:
        {input}
        """;

    try
    {
        Console.WriteLine();
        Console.WriteLine("AI is processing your request...");
        Console.WriteLine();

        var response = await chatClient.GetResponseAsync(
            prompt,
            cancellationToken: cancellationToken);

        Console.WriteLine("========== AI RESPONSE ==========");
        Console.WriteLine(response.Text);
        Console.WriteLine("=================================");
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("The AI request was cancelled.");
        throw;
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine(
            "Could not connect to Ollama. Check that Ollama is running.");
        Console.WriteLine($"Details: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine("An unexpected error occurred.");
        Console.WriteLine($"Details: {ex.Message}");
    }
}