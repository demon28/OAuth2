using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Entities
{
    public class TokenRightApi
    {
        public int Right_Id { get; set; }
        public int Token_Id { get; set; }
        public int Api_Id { get; set; }
        public DateTime Create_Time { get; set; }
        public DateTime Last_Modify_Time { get; set; }
        public int Have_Right { get; set; }
        public DateTime Expire_Time { get; set; }
        public string Api_Url { get; set; }
        public string Api_Name { get; set; }
        public int Status { get; set; }

    }
}
