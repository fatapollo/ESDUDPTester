// See https://aka.ms/new-console-template for more information
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class StringReceiver
{
    private readonly int _port;

    public StringReceiver(int port)
    {
        _port = port;
    }

    public void StartListening()
    {
        using (var udpClient = new UdpClient(_port))
        {
            Console.WriteLine($"Listening for UDP messages on port {_port}...");

            while (true)
            {
                try
                {
                    // Receive the data
                    var remoteEndpoint = new IPEndPoint(IPAddress.Any, _port);
                    var receivedBytes = udpClient.Receive(ref remoteEndpoint);

                    // Convert bytes to string
                    var receivedMessage = Encoding.UTF8.GetString(receivedBytes);

                    // Print the received message
                    Console.WriteLine($"Received message from {remoteEndpoint}: {receivedMessage}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error receiving message: {ex.Message}");
                }
            }
        }
    }

    public static void Main(string[] args)
    {
        // Specify the port to listen on
        int port = 1992;

        // Create and start the receiver
        var receiver = new StringReceiver(port);
        receiver.StartListening();
    }
}
