using System;

namespace VTCManager_1._0._0.Objekte
{
    public class User
    {
        public int userID;
        public String company;
        public String username;
        public String profile_picture;
        public int driven_tours;
        public int bank_balance;
        public int patreon_state;
        public String authcode;
        public Translation translation;

        public User(int userID, String company, String username,String profile_picture, int driven_tours, int bank_balance, int patreon_state,String authcode, Translation translation)
        {
            this.translation = translation;
            this.userID = userID;
            this.company = (company == "0") ? this.company = this.translation.no_company_text : this.company = company;
            this.username = username;
            this.profile_picture = profile_picture;
            this.driven_tours = driven_tours;
            this.bank_balance = bank_balance;
            this.patreon_state = patreon_state;
            this.authcode = authcode;
        }
    }
}
