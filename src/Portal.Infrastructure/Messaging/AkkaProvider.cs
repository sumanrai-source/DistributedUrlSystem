using Akka.Actor;
using Portal.Application.Actors;
using Portal.Application.IRepository;
using Shared.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Infrastructure.Messaging
{
    public class AkkaProvider : IAkkaActorProvider
    {
        private readonly ActorSelection _urlCreatedListener;

        public AkkaProvider(ActorSystem actorSystem)
        {
            _urlCreatedListener = actorSystem.ActorSelection(
                "akka.tcp://AssignerSystem@localhost:4054/user/urlCreatedListener");

            TestConnection(_urlCreatedListener);
        }

        private async void TestConnection(ActorSelection actorSelection)
        {
            try
            {
                var actor = await actorSelection.ResolveOne(
                    TimeSpan.FromSeconds(5));

                Console.WriteLine(
                    $"Remote actor resolved: {actor.Path}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Remote actor not found: {ex.Message}");
            }
        }

        public void Publish(UrlCreatedEvent createdEvents)
        {
            _urlCreatedListener.Tell(createdEvents);
        }
    }
}
