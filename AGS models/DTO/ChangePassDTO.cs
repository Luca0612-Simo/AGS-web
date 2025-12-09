using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS_Models.DTO
{
    public class ChangePassDTO
    {
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}