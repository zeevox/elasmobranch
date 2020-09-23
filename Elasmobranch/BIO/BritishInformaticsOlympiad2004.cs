using System;

namespace Elasmobranch.BIO
{
    public class BritishInformaticsOlympiad2004
    {
        public static DateTime MayanToGregorian(int baktuns, int katuns, int tuns, int uinals, int kins)
        {
            baktuns -= 13;
            katuns -= 20;
            tuns -= 7;
            uinals -= 16;
            kins -= 3;
            
            return new DateTime(2000, 1, 1)
                .AddDays(baktuns * 20 * 20 * 18 * 20 + 
                         katuns       * 20 * 18 * 20 + 
                         tuns              * 18 * 20 + 
                         uinals                 * 20 +
                         kins                         );
        }
    }
}