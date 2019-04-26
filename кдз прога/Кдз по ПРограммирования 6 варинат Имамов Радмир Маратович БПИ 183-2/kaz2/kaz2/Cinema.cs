using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kaz2
{
    public class Cinema
    {
        public District distr;
        public string RowNum;
        public string CommonName;
        public string FullName;
        public string ShortName;
        public string ChiefOrg;
        public string Address;
        public string ChiefName;
        public string ChiefPosition;
        public string PublicPhone;
        public string Fax;
        public string Email;
        public string WorkingHours;
        public string CvalifivationOfWOrkingHours;
        public string WebSite;
        public string OKPO;
        public string INN;
        public string NumberOfHalls;
        public string TotalSeatsAmount;
        public string X_WGS;
        public string Y_WGS;
        public string GlobalID;

        public Cinema(District district, string rowNum, string commonName, string fullName, string shortName, string chiefOrg, string address, string chiefName, string chiefPosition, string publicPhone, string fax, string email, string workingHours, string cvalifivationOfWOrkingHours, string webSite, string oKPO, string iNN, string numberOfHalls, string totalSeatsAmount, string x_WGS, string y_WGS, string globalID)
        {
            distr = district;
            RowNum = rowNum;
            CommonName = commonName;
            FullName = fullName;
            ShortName = shortName;
            ChiefOrg = chiefOrg;
            Address = address;
            ChiefName = chiefName;
            ChiefPosition = chiefPosition;
            PublicPhone = publicPhone;
            Fax = fax;
            Email = email;
            WorkingHours = workingHours;
            CvalifivationOfWOrkingHours = cvalifivationOfWOrkingHours;
            WebSite = webSite;
            OKPO = oKPO;
            INN = iNN;
            NumberOfHalls = numberOfHalls;
            TotalSeatsAmount = totalSeatsAmount;
            X_WGS = x_WGS;
            Y_WGS = y_WGS;
            GlobalID = globalID;
        }
    }
    public class District
    {
        public string AdmArea;
        public string district;

        public District(string admArea, string district)
        {
            AdmArea = admArea;
            this.district = district;
        }

        public static int CountDistrictInArea(string[][] arr, string area)
        {
            int count = 0;
            List<string> dis = new List<string>();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                if(arr[i][5]==area)
                {
                    bool flag = false;
                    for (int j = 0; j < dis.Count; j++)
                    {
                        if (string.Compare(arr[i][6], dis[j])==0) flag = true;
                    }
                    dis.Add(arr[i][6]);
                    if (flag) continue;
                    else count++;

                }
            }
            return count;
        }
    }
}
