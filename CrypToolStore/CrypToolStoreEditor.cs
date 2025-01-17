﻿/*
   Copyright 2018 Nils Kopal <Nils.Kopal<AT>Uni-Kassel.de>

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using CrypTool.PluginBase;
using CrypTool.PluginBase.Attributes;
using CrypTool.PluginBase.Editor;
using CrypTool.PluginBase.Miscellaneous;
using System;
using System.Windows.Controls;

namespace CrypTool.CrypToolStore
{
    [TabColor("LightSeaGreen")]
    [EditorInfo("CrypToolstore", false, true, false, true, false, false)]
    [Author("Nils Kopal", "kopal@CrypTool.org", "CrypTool Team", "http://www.CrypTool.org")]
    [PluginInfo("CrypTool.CrypToolStore.Properties.Resources", "PluginCaption", "PluginTooltip", null, "CrypToolStore/icon_small.png")]
    public class CrypToolStoreEditor : IEditor
    {
        private CrypToolStorePresentation _presentation;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CrypToolStoreEditor()
        {            
            _presentation  = new CrypToolStorePresentation(this);  
        }

        /// <summary>
        /// Initialize method
        /// </summary>
        public void Initialize()
        {

        }        

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {

        }

        public UserControl Presentation
        {
            get { return _presentation; }
        }

        /// <summary>
        /// Logs to CT2 gui log
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        internal void GuiLogMessage(string message, NotificationLevel logLevel)
        {
            EventsHelper.GuiLogMessage(OnGuiLogNotificationOccured, this, new GuiLogEventArgs(message, this, logLevel));
        }


        #region unused methods

        public void New()
        {
            
        }

        public void Open(string fileName)
        {
            
        }

        public void Save(string fileName)
        {
            
        }

        public void Add(Type type)
        {
            
        }

        public void Undo()
        {
            
        }

        public void Redo()
        {
            
        }

        public void Cut()
        {
            
        }

        public void Copy()
        {
            
        }

        public void Paste()
        {
           
        }

        public void Remove()
        {
            
        }

        public void Print()
        {
            
        }

        public void AddText()
        {
            
        }

        public void AddImage()
        {
            
        }

        public void ShowSelectedEntityHelp()
        {
            
        }

        public bool CanUndo
        {
            get { return false; }
        }

        public bool CanRedo
        {
            get { return false; }
        }

        public bool CanCut
        {
            get { return false; }
        }

        public bool CanCopy
        {
            get { return false; }
        }

        public bool CanPaste
        {
            get { return false; }
        }

        public bool CanRemove
        {
            get { return false; }
        }

        public bool CanExecute
        {
            get { return false; }
        }

        public bool CanStop
        {
            get { return false; }
        }

        public bool HasChanges
        {
            get { return false; }
        }

        public bool CanPrint
        {
            get { return false; }
        }

        public bool CanSave
        {
            get { return false; }
        }

        public string CurrentFile
        {
            get { return null; }
        }

        public string SamplesDir
        {
            set { }
        }

        public bool ReadOnly
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        public bool HasBeenClosed
        {
            get;
            set;
        }

        public Core.PluginManager PluginManager
        {
            get;
            set;
        }        

        public PluginBase.ISettings Settings
        {
            get { return null; }
        }       

        public void Execute()
        {
            
        }

        public void Stop()
        {
            
        }

        #endregion

        #region events

        public event PluginBase.GuiLogNotificationEventHandler OnGuiLogNotificationOccured;
        public event PluginBase.SelectedPluginChangedHandler OnSelectedPluginChanged;
        public event PluginBase.ProjectTitleChangedHandler OnProjectTitleChanged;
        public event PluginBase.OpenProjectFileHandler OnOpenProjectFile;
        public event PluginBase.OpenTabHandler OnOpenTab;
        public event PluginBase.OpenEditorHandler OnOpenEditor;
        public event PluginBase.FileLoadedHandler OnFileLoaded;
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
