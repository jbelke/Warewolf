
/*
*  Warewolf - The Easy Service Bus
*  Copyright 2016 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Windows;
using Dev2.Common;
using Dev2.Common.Interfaces.PopupController;
using Dev2.Studio.ViewModels.Dialogs;

// ReSharper disable CheckNamespace
namespace Dev2.Studio.Controller
{
    public class PopupController : Common.Interfaces.Studio.Controller.IPopupController
    {
        public string Header { get; set; }

        public string Description { get; set; }

        public string Question { get; set; }

        public MessageBoxImage ImageType { get; set; }

        public MessageBoxButton Buttons { get; set; }

        public string DontShowAgainKey { get; set; }

        public bool IsError { get; set; }
        public bool IsInfo { get; set; }
        public bool IsQuestion { get; set; }
        public bool IsDependenciesButtonVisible { get; set; }

        public MessageBoxResult Show(IPopupMessage popupMessage)
        {
            return Show(popupMessage.Description, popupMessage.Header, popupMessage.Buttons, popupMessage.Image, popupMessage.DontShowAgainKey, popupMessage.IsDependenciesButtonVisible, popupMessage.IsError, popupMessage.IsInfo, popupMessage.IsQuestion);
        }
        
        public MessageBoxResult Show()
        {
            return ShowDev2MessageBox(Description, Header, Buttons, ImageType, DontShowAgainKey, IsDependenciesButtonVisible, IsError, IsInfo, IsQuestion);
        }

        public MessageBoxResult Show(string description, string header = "", MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage image = MessageBoxImage.Asterisk, string dontShowAgainKey = null, bool isDependenciesButtonVisible = false, bool isError = false, bool isInfo = false, bool isQuestion = false)
        {
            Buttons = buttons;
            Description = description;
            Header = header;
            ImageType = image;
            DontShowAgainKey = dontShowAgainKey;
            IsDependenciesButtonVisible = isDependenciesButtonVisible;
            IsError = isError;
            IsInfo = isInfo;
            IsQuestion = isQuestion;
            return Show();
        }

        public Func<string, string, MessageBoxButton, MessageBoxImage, string, bool, bool, bool, bool, MessageBoxResult> ShowDev2MessageBox = (description, header, buttons, imageType, dontShowAgainKey, isDependenciesButtonVisible, isError, isInfo, isQuestion) => Dev2MessageBoxViewModel.Show(description, header, buttons, imageType, dontShowAgainKey, isDependenciesButtonVisible, isError, isInfo, isQuestion);

        public MessageBoxResult ShowNotConnected()
        {
            Buttons = MessageBoxButton.OK;
            Header = "Server is not connected";
            Description = "You can not change the settings for a server that is offline.";
            ImageType = MessageBoxImage.Error;
            IsDependenciesButtonVisible = false;
            IsInfo = false;
            IsError = true;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowServerNotConnected(string server)
        {
            Buttons = MessageBoxButton.OK;
            Header = "Server is not connected";
            Description = "The server "+ server +" is unreachable and will be removed form your explorer tab. Please reconnect to save any unsaved work.";
            ImageType = MessageBoxImage.Error;
            IsDependenciesButtonVisible = false;
            IsInfo = false;
            IsError = true;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowDeleteConfirmation(string nameOfItemBeingDeleted)
        {
            Buttons = MessageBoxButton.YesNo;
            Header = "Are you sure?";
            Description = "Are you sure you want to delete " + nameOfItemBeingDeleted + "?";
            ImageType = MessageBoxImage.Warning;
            IsDependenciesButtonVisible = false;
            IsInfo = true;
            IsError = false;
            IsQuestion = false;
            return Show();
        }
        public MessageBoxResult ShowExceptionViewAppreciation()
        {
            Buttons = MessageBoxButton.OK;
            Header = "We�ve got your feedback!";
            Description = "Thank you for taking the time to log it. Follow the issue " + Environment.NewLine + 
                "in the Community to keep updated on the progress.";
            ImageType = MessageBoxImage.Information;
            IsDependenciesButtonVisible = false;
            IsInfo = true;
            IsError = false;
            IsQuestion = false;
            return Show();
        }
        public MessageBoxResult ShowCorruptTaskResult(string errorMessage)
        {
            Buttons = MessageBoxButton.OK;
            Header = "Scheduler load error";
            Description = "Unable to retrieve tasks." + Environment.NewLine + 
                          "ERROR: " + errorMessage + ". " + Environment.NewLine + 
                          "Please check that there a no corrupt files."  + Environment.NewLine + 
                         @"C:\Windows\System32\Tasks\Warewolf";
            ImageType = MessageBoxImage.Error;
            IsDependenciesButtonVisible = false;
            IsInfo = false;
            IsError = true;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowNameChangedConflict(string oldName, string newName)
        {
            Buttons = MessageBoxButton.YesNoCancel;
            Header = "Rename conflict";
            Description = "The following task has been renamed " + oldName + " -> " + newName + ". You will lose the history for the old task." + Environment.NewLine +
                          " Would you like to save the new name?" + Environment.NewLine +
                          "-----------------------------------------------------------------" +
                              Environment.NewLine +
                          "Yes - Save with the new name." + Environment.NewLine +
                          "No - Save with the old name." + Environment.NewLine +
                          "Cancel - Returns you to Scheduler.";
            ImageType = MessageBoxImage.Information;
            IsDependenciesButtonVisible = false;
            IsInfo = true;
            IsError = false;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowDeployConflict(int conflictCount)
        {
            string correctDesc = String.Empty;
            Buttons = MessageBoxButton.OKCancel;
            Header = "Deploy conflicts";
            if (conflictCount == 1)
            {
                correctDesc = "There is [ " + conflictCount + " ] conflict that occurs";
            }
            if (conflictCount > 1)
            {
                correctDesc = "There are [ " + conflictCount + " ] conflicts that occur";
            }
            Description = correctDesc + " in this deploy." + Environment.NewLine + "Click OK to override the conflicts or Cancel to view the conflicting resources." + Environment.NewLine +
                          "--------------------------------------------------------------------------------" +
                              Environment.NewLine +
                          "OK - Continue to deploy resources." + Environment.NewLine +
                          "Cancel - Cancel the deploy and view the conflicts.";
            ImageType = MessageBoxImage.Information;
            IsDependenciesButtonVisible = false;
            IsInfo = true;
            IsError = false;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowDeployServerVersionConflict(string sourceServerVersion, string destinationServerVersion)
        {
            Buttons = MessageBoxButton.OKCancel;
            Header = "Deploy Version conflicts";
            Description = "There is a conflict between the two versions in this deploy." +
                Environment.NewLine + "Source Server Version: " + sourceServerVersion +
                Environment.NewLine + "Destination Server Version: " + destinationServerVersion +
                Environment.NewLine + "Click OK to continue or Cancel to return." + 
                Environment.NewLine +
                          "--------------------------------------------------------------------------------" +
                              Environment.NewLine +
                          "OK - Continue to deploy resources." + Environment.NewLine +
                          "Cancel - Cancel the deploy.";
            ImageType = MessageBoxImage.Information;
            IsDependenciesButtonVisible = false;
            IsInfo = true;
            IsError = false;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowDeployServerMinVersionConflict(string sourceServerVersion, string destinationServerVersion)
        {
            Buttons = MessageBoxButton.OKCancel;
            Header = "Deploy Version conflicts";
            Description = "There is a conflict between the two versions in this deploy." +
                Environment.NewLine + "Source Server Version: " + sourceServerVersion +
                Environment.NewLine + "Destination Minimum supported version: " + destinationServerVersion +
                Environment.NewLine + "The destination server does not support all the same features as the source server and your deployment is not guaranteed to work. " +
                Environment.NewLine + "Click OK to continue or Cancel to return." +
                Environment.NewLine +
                          "--------------------------------------------------------------------------------" +
                              Environment.NewLine +
                          "OK - Continue to deploy resources." + Environment.NewLine +
                          "Cancel - Cancel the deploy.";
            ImageType = MessageBoxImage.Information;
            IsDependenciesButtonVisible = false;
            IsInfo = true;
            IsError = false;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowConnectServerVersionConflict(string selectedServerVersion, string currentServerVersion)
        {
            Buttons = MessageBoxButton.OK;
            Header = "Server Version conflict";
            Description = "There is a version conflict with the current selected server." + Environment.NewLine + 
                Environment.NewLine + "Selected Server Version: " + selectedServerVersion +
                Environment.NewLine + "Current Server Version: " + currentServerVersion + Environment.NewLine + 
                Environment.NewLine + "Please make sure that the server you are trying to connect to has the latest version.";
            ImageType = MessageBoxImage.Error;
            IsDependenciesButtonVisible = false;
            IsInfo = false;
            IsError = true;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowDeployResourceNameConflict(string conflictResourceName)
        {
            Buttons = MessageBoxButton.OK;
            Header = "Deploy ResourceName conflicts";
            Description = "There is a conflict between the two resources in this deploy." +
                Environment.NewLine + "Conflict Resource Name: " + conflictResourceName +
                Environment.NewLine + "Click OK and rename the conflicting resource/s." +
                Environment.NewLine +
                          "--------------------------------------------------------------------------------" +
                              Environment.NewLine +
                          "OK - Cancel the deploy.";
            ImageType = MessageBoxImage.Information;
            IsDependenciesButtonVisible = false;
            IsInfo = true;
            IsError = false;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowDeployNameConflict(string message)
        {

            Buttons = MessageBoxButton.OKCancel;
            Header = "Deploy Name conflicts";

            Description = message;
            ImageType = MessageBoxImage.Error;
            IsDependenciesButtonVisible = false;
            IsInfo = false;
            IsError = true;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowSettingsCloseConfirmation()
        {

            Header = "Settings have changed";
            var description = "Settings have not been saved." + Environment.NewLine
                              + "Would you like to save the settings? " + Environment.NewLine +
                              "-----------------------------------------------------------------" +
                              Environment.NewLine +
                              "Yes - Save the settings." + Environment.NewLine +
                              "No - Discard your changes." + Environment.NewLine +
                              "Cancel - Returns you to settings.";
            Description = description;
            Buttons = MessageBoxButton.YesNoCancel;
            ImageType = MessageBoxImage.Information;
            IsDependenciesButtonVisible = false;
            IsInfo = true;
            IsError = false;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowSchedulerCloseConfirmation()
        {
            Header = "Scheduler Task has changes";
            var description = "Scheduler Task has not been saved." + Environment.NewLine
                              + "Would you like to save the Task? " + Environment.NewLine +
                              "-----------------------------------------------------------------" +
                              Environment.NewLine +
                              "Yes - Save the Task." + Environment.NewLine +
                              "No - Discard your changes." + Environment.NewLine +
                              "Cancel - Returns you to Scheduler.";
            Description = description;
            Buttons = MessageBoxButton.YesNoCancel;
            ImageType = MessageBoxImage.Information;
            IsDependenciesButtonVisible = false;
            IsInfo = true;
            IsError = false;
            IsQuestion = false;
            return Show();
        }
        
        public MessageBoxResult ShowItemCloseCloseConfirmation(string nameOfItem)
        {
            Header = string.Format("{0} Changes not saved", nameOfItem);
            var description = nameOfItem+" has not been saved." + Environment.NewLine
                              + "Would you like to save the Task? " + Environment.NewLine +
                              "-----------------------------------------------------------------" +
                              Environment.NewLine +
                              "Yes - Save "+ nameOfItem +"." + Environment.NewLine +
                              "No - Discard your changes." + Environment.NewLine +
                              "Cancel - Returns you to "+nameOfItem+".";
            Description = description;
            Buttons = MessageBoxButton.YesNoCancel;
            ImageType = MessageBoxImage.Information;
            IsDependenciesButtonVisible = false;
            IsInfo = true;
            IsError = false;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowItemSourceCloseConfirmation(string nameOfItem)
        {
            Header = "Changes not saved";
            var description = "Your changes have not been saved." + Environment.NewLine
                              + "Would you like to save? " + Environment.NewLine +
                              "-----------------------------------------------------------------" +
                              Environment.NewLine +
                              "Yes - Save your changes." + Environment.NewLine +
                              "No - Discard your changes." + Environment.NewLine +
                              "Cancel - Returns you to the tab.";
            Description = description;
            Buttons = MessageBoxButton.YesNoCancel;
            ImageType = MessageBoxImage.Information;
            IsDependenciesButtonVisible = false;
            IsInfo = true;
            IsError = false;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowNoInputsSelectedWhenClickLink()
        {
            Header = "Did you know?";
            var description = "You can pass variables into your workflow" + Environment.NewLine
                              + "by selecting the Input checkbox" + Environment.NewLine +
                              "in the Variables window.";
            Description = description;
            Buttons = MessageBoxButton.OK;
            ImageType = MessageBoxImage.Information;
            DontShowAgainKey = GlobalConstants.Dev2MessageBoxNoInputsWhenHyperlinkClickedDialog;
            IsDependenciesButtonVisible = false;
            IsInfo = true;
            IsError = false;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowSaveErrorDialog(string errorMessage)
        {
            Header = "Saving Error";
            var description = "The following error occurred on save:" + Environment.NewLine
                              + errorMessage;
            Description = description;
            Buttons = MessageBoxButton.OK;
            ImageType = MessageBoxImage.Error;
            IsDependenciesButtonVisible = false;
            IsInfo = false;
            IsError = true;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowConnectionTimeoutConfirmation(string serverName)
        {
            Header = "Server is unreachable";
            var description = " Unable to reach " + serverName + ": Connection timed out." + Environment.NewLine
                              + " Make sure the remote computer is powered on." + Environment.NewLine
                              + Environment.NewLine
                              + " Would you like to re-try? " + Environment.NewLine;
            Description = description;
            Buttons = MessageBoxButton.YesNo;
            ImageType = MessageBoxImage.Error;
            IsDependenciesButtonVisible = false;
            IsInfo = false;
            IsError = true;
            IsQuestion = false;
            return Show();
        }

        public void ShowInvalidCharacterMessage(string invalidText)
        {
            Description = string.Format("{0} is invalid. Warewolf only supports latin characters", invalidText);
            Header = "Invalid text";
            Buttons = MessageBoxButton.OK;
            ImageType = MessageBoxImage.Error;
            IsDependenciesButtonVisible = false;
            IsInfo = false;
            IsError = true;
            IsQuestion = false;
            Show();
        }

        public MessageBoxResult ShowDeleteVersionMessage(string displayName)
        {
            Header = "Delete version";
            var description = string.Format("Are you sure you want to delete {0}?", displayName);
            Description = description;
            Buttons = MessageBoxButton.YesNo;
            ImageType = MessageBoxImage.Warning;
            IsDependenciesButtonVisible = false;
            IsInfo = true;
            IsError = false;
            IsQuestion = false;
            return Show();
        }

        public MessageBoxResult ShowRollbackVersionMessage(string displayName)
        {
            Header = "Make current version";
            var description = string.Format("{0} will become the current version.{1}Do you want to proceed ?", displayName, Environment.NewLine);
            Description = description;
            Buttons = MessageBoxButton.YesNo;
            ImageType = MessageBoxImage.Warning;
            IsDependenciesButtonVisible = false;
            IsInfo = true;
            IsError = false;
            IsQuestion = false;
            return Show();
        }
    }
}
