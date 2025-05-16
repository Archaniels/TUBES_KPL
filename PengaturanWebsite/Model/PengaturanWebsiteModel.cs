using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUBES_KPL.PengaturanWebsite.Model
{
    public class PengaturanWebsiteModel
    {
        public enum PengaturanWebsiteState
        {
            MainMenu,
            GeneralSettings,
            ContentSettings,
            Saving,
            Exit
        }

        // Enum untuk mendefinisikan event pada automata
        public enum PengaturanWebsiteEvent
        {
            SelectGeneral,
            SelectContent,
            Save,
            Back,
            Quit
        }

        // Kelas yang merepresentasikan Automata untuk navigasi pengaturan website
        public class PengaturanWebsiteAutomata
        {
            

            // Reset state machine ke state awal
            public void Reset()
            {
                CurrentState = PengaturanWebsiteState.MainMenu;
            }
        }
    }
}
