/*
   Copyright 2008 Timo Eckhardt, University of Siegen

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

using Primes.Properties;

namespace Primes.Options
{
    public static class OptionsAccess
    {
        public static void ShowOptionsDialog()
        {
            OptionsWindow.ShowOptionsDialog();
        }

        public static void ForceClose()
        {
            OptionsWindow.ForceClosing();
        }

        public static bool UsePari
        {
            get { return new Settings().usePari; }
        }

        public static bool UseSimpson
        {
            get { return new Settings().useSimpson; }
        }

        public static string GpExe
        {
            get { return new Settings().gpexe; }
        }
    }
}
