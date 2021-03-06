﻿using HelperLibrary.Logging;
using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace HelperLibrary.Networking.ClientServer
{
    public abstract class SslServer : Server
    {
        private readonly X509Certificate2 _certificate;

        /// <summary>
        /// Initializes a new <see cref="SslServer"/> with <see cref="X509Certificate2"/> on the given port and IP address.
        /// </summary>
        /// <param name="certificate"><see cref="X509Certificate2"/> for authentication and encryption.</param>
        /// <param name="ip">Ip to setup.</param>
        /// <param name="port">Port to setup.</param>
        protected SslServer(X509Certificate2 certificate, string ip, int port) : base(ip, port)
        {
            _certificate = certificate;
        }

        protected SslServer(X509Certificate2 certificate, IPAddress ip, int port) : base(ip, port)
        {
            _certificate = certificate;
        }

        protected override void ListenOnNewClients()
        {
            Listener.Start();

            while (true)
            {
                TcpClient connectedClient = Listener.AcceptTcpClient();
                var sslStream = ProcessClient(connectedClient);

                var client = HandleNewConnectedClient(connectedClient, sslStream);
                
                Clients.Add(client);                
                OnClientConnected(new ClientConnectedEventArgs(client));
                Log.Info("New Client connected (IP: " + connectedClient.Client.RemoteEndPoint + ")");
            }
        }

        private SslStream ProcessClient(TcpClient client)
        {
            // A client has connected. Create the 
            // SslStream using the client's network stream.
            SslStream sslStream = new SslStream(client.GetStream(), false);
            // Authenticate the server but don't require the client to authenticate.
            try
            {
                sslStream.AuthenticateAsServer(_certificate, false, SslProtocols.Tls, true);
                // Display the properties and settings for the authenticated stream.
                Log.SslStreamInformation(sslStream);

                return sslStream;
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                Console.WriteLine("Authentication failed - closing the connection.");
                sslStream.Close();
                client.Close();

                return null;
            }            
        }
    }
}
