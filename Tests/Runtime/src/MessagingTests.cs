using System.Collections;
using RGN.Extensions;
using RGN.Modules.Messaging;
using RGN.Tests;
using UnityEngine.TestTools;

namespace RGN.Messaging.Tests.Runtime
{
    public class MessagingTests : BaseTests
    {
        [UnityTest]
        public IEnumerator SendMessageByUserId_WorksOnlyForAdminUsers()
        {
            yield return LoginAsAdminTester();

            string appPackageName = RGNCore.I.AppIDForRequests;
            string userId = "cbcadb4ccebd4dd689d50b56be8b9f9d";
            string title = "Hello from tests!";
            string body = "The runtime tests are great!";

            var task = MessagingModule.I.SendMessageByUserId(appPackageName, userId, title, body);
            yield return task.AsIEnumeratorReturnNull();
        }
    }
}
