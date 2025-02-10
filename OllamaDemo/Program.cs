using Azure;
using Codeblaze.SemanticKernel.Connectors.Ollama;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.VisualBasic;
using System.ClientModel;

namespace OllamaDemo
{
    internal class Program
    {
    static async Task Main(string[] args)
    {
        var builder = Kernel.CreateBuilder().AddOllamaChatCompletion("deepseek-r1:32b", "http://192.168.0.120:11434");

        builder.Services.AddScoped<HttpClient>();
        var kernel = builder.Build();

        while (true)
        {
            string input = "";
            Console.Write("请输入: ");
            input = Console.ReadLine();
            Console.WriteLine("");
            var contents = kernel.InvokePromptStreamingAsync(input);

            if (contents == null)
            {
                Console.WriteLine("Error: 内容为空!");
                continue;
            }
            else
            {
                Console.WriteLine($"\nDeepseek: \n");
                await foreach (var item in contents)
                {
                    Console.Write(item.ToString());
                }
            }
            Console.WriteLine("");
        }
    }
    }
}

