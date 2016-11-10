using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models.PlatoonViewModels
{
    public class PlatoonViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "账号（手机号）")]
        [RegularExpression(@"^1[3|4|5|7|8]\d{9}$", ErrorMessage = "请输入正确的手机号码")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "是否启用")]
        public bool Enabled { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        public string Name { get; set; }

        [Display(Name = "性别")]
        public bool Gender { get; set; }   // 性别：true-男；false-女

        [Display(Name = "身份证号")]
        [RegularExpression(@"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$", ErrorMessage = "请输入正确的身份张号码")]
        public string IdCardNumber { get; set; }    // 身份证号

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy年M月d日}")]
        [Display(Name = "生日")]
        public DateTime? Birthday { get; set; }  // 生日

        [Display(Name = "籍贯")]
        [StringLength(16, ErrorMessage = "{0}的长度为不超过{1}个字符")]
        public string NativePlace { get; set; } // 籍贯

        [Display(Name = "健康状况")]
        [StringLength(32, ErrorMessage = "{0}的长度为不超过{1}个字符")]
        public string HealthStatus { get; set; }    // 健康状况

        [Display(Name = "QQ号码")]
        [RegularExpression(@"^[1-9]\d{4,9}$", ErrorMessage = "请输入合理的QQ号码")]
        public string QQ { get; set; }    // QQ号

        [Display(Name = "微信账号")]
        [StringLength(64, ErrorMessage = "{0}的长度为不超过{1}个字符")]
        public string Wechat { get; set; }  // 微信号

        [Display(Name = "支付宝")]
        [StringLength(64, ErrorMessage = "{0}的长度为不超过{1}个字符")]
        public string Alipay { get; set; }  // 支付宝

        
        [Display(Name = "退伍前职务")]
        [StringLength(32, ErrorMessage = "{0}的长度为不超过{1}个字符")]
        public string ArmyPost { get; set; } // 退伍前职务

        [Display(Name = "退伍前军衔")]
        [StringLength(32, ErrorMessage = "{0}的长度为不超过{1}个字符")]
        public string ArmyRank { get; set; }    // 退伍前军衔

        [Display(Name = "入伍时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy年M月}")]
        [DataType(DataType.Date)]
        public DateTime? MilitaryTime { get; set; }  // 入伍时间

        [Display(Name = "退伍时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy年M月}")]
        [DataType(DataType.Date)]
        public DateTime? DischargeTime { get; set; } // 退伍时间


        public PlatoonViewModel()
        {

        }

        public PlatoonViewModel(Platoon platoon)
        {
            Id = platoon.Id;
            UserName = platoon.UserName;
            Password = "Hidden";
            Name = platoon.Name;
            Gender = platoon.Gender;
            IdCardNumber = platoon.IdCardNumber;
            Birthday = platoon.Birthday;
            NativePlace = platoon.NativePlace;
            HealthStatus = platoon.HealthStatus;
            QQ = platoon.QQ;
            Wechat = platoon.Wechat;
            Alipay = platoon.Alipay;
            ArmyPost = platoon.ArmyPost;
            ArmyRank = platoon.ArmyRank;
            MilitaryTime = platoon.MilitaryTime;
            DischargeTime = platoon.DischargeTime;
            Enabled = platoon.Enabled;
        }

        public Platoon ToPlatoon()
        {
            Platoon platoon = new Platoon
            {
                UserName = UserName,
                Name = Name,
                Gender = Gender,
                IdCardNumber = IdCardNumber,
                Birthday = Birthday,
                NativePlace = NativePlace,
                HealthStatus = HealthStatus,
                QQ = QQ,
                Wechat = Wechat,
                Alipay = Alipay,
                ArmyPost = ArmyPost,
                ArmyRank = ArmyRank,
                MilitaryTime = MilitaryTime,
                DischargeTime = DischargeTime,
                Enabled = Enabled,
            };

            if (Id != null)
                platoon.Id = Id;

            return platoon;
        }

        public void Update(ref Platoon platoon)
        {
            platoon.UserName = UserName;
            platoon.Name = Name;
            platoon.Gender = Gender;
            platoon.IdCardNumber = IdCardNumber;
            platoon.Birthday = Birthday;
            platoon.NativePlace = NativePlace;
            platoon.HealthStatus = HealthStatus;
            platoon.QQ = QQ;
            platoon.Wechat = Wechat;
            platoon.Alipay = Alipay;
            platoon.ArmyPost = ArmyPost;
            platoon.ArmyRank = ArmyRank;
            platoon.MilitaryTime = MilitaryTime;
            platoon.DischargeTime = DischargeTime;
            platoon.Enabled = Enabled;
        }
    }
}
