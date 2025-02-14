using Discord;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;


namespace Discord_test
{
    public class Program
    {
        private static DiscordSocketClient _client;
        public static async Task Main()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;

            var token = "";
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public class LoggingService
        {
            public LoggingService(DiscordSocketClient client, CommandService command)
            {
                client.Log += LogAsync;
                command.Log += LogAsync;
            }
            private Task LogAsync(LogMessage message)
            {
                if (message.Exception is CommandException cmdException)
                {
                    Console.WriteLine($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()}"
                        + $" failed to execute in {cmdException.Context.Channel}.");
                    Console.WriteLine(cmdException);
                }
                else
                    Console.WriteLine($"[General/{message.Severity}] {message}");

                return Task.CompletedTask;
            }
        }
            private static async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
            {
                var message = await before.GetOrDownloadAsync();
                Console.WriteLine($"{message} -> {after}");
            }
        }

    }