﻿using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Warewolf.UITests
{
    [CodedUITest]
    public class DeployTests
    {
        const string ServerSourceName = "TSTCIREMOTE";
        const string LocalWorkflowName = "RemoteServerUITestWorkflow";
        const string RemoteSubWorkflowName = "workflow1";
        const string LocalWorkflow = "LocalWorkflow";
        const string WindowsGroup = "Domain Users";
        private const string ServerAddress = "tst-ci-";

        [TestMethod]
        public void Deploy_WorkFlow_To_Remote_Server()
        {
            Uimap.Click_New_Workflow_Ribbon_Button();
            Uimap.Drag_Toolbox_MultiAssign_Onto_DesignSurface();
            Uimap.Save_With_Ribbon_Button_And_Dialog(LocalWorkflow);
            Uimap.Click_Deploy_Ribbon_Button();
            Uimap.Click_Deploy_Tab_Destination_Server_Combobox();
        }


        #region Additional test attributes

        [TestInitialize()]
        public void MyTestInitialize()
        {
            Uimap.SetGlobalPlaybackSettings();
            Uimap.WaitForStudioStart();
            Console.WriteLine("Test \"" + TestContext.TestName + "\" starting on " + System.Environment.MachineName);
        }
        
        [TestCleanup()]
        public void MyTestCleanup()
        {
            Playback.PlaybackError -= Uimap.OnError;
            //Uimap.TryCloseHangingSaveDialog();
            Uimap.TryRemoveFromExplorer(LocalWorkflow);
            //Uimap.TryDisconnectFromRemoteServerAndRemoveSourceFromExplorer(ServerSourceName);
            //Uimap.TryCloseAllTabs();
        }

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        private TestContext testContextInstance;

        UIMap Uimap
        {
            get
            {
                if (_uiMap == null)
                {
                    _uiMap = new UIMap();
                }

                return _uiMap;
            }
        }

        private UIMap _uiMap;

        #endregion
    }
}