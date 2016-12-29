using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Stb.Data.Models;
using Stb.Platform.Models.DistrictViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.WorkerViewModels
{
    public class WorkerViewModel
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

        [Display(Name = "头像")]
        public string Portrait { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        public string Name { get; set; }

        [Display(Name = "性别")]
        public bool Gender { get; set; } = true;  // 性别：true-男；false-女

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

        [Display(Name = "是否班长")]
        public bool IsHeader { get; set; }    // 是否班长

        [Display(Name = "候选班长")]
        public bool IsCandidate { get; set; }    // 是否候选班长

        [Display(Name = "班长")]
        public string HeaderId { get; set; }  // 班长Id

        [Display(Name = "班长")]
        public string HeaderName { get; set; }  // 班长姓名


        [Display(Name = "专业技能")]
        public List<JobClassViewModel> JobClasses { get; set; }

        [Display(Name = "服务范围")]
        public List<DistrictViewModel> Districts { get; set; }

        public List<WorkerViewModel> Workers { get; set; }

        [Display(Name = "擅长技能")]
        public int? BestJobClassId { get; set; } // 擅长技能

        [Display(Name = "擅长技能")]
        public string BestJobClassName { get; set; } // 擅长技能名称


        public WorkerViewModel()
        {

        }

        public WorkerViewModel(Worker worker)
        {
            Id = worker.Id;
            UserName = worker.UserName;
            Password = "Hidden";
            Portrait = worker.Portrait;
            Name = worker.Name;
            Gender = worker.Gender;
            IdCardNumber = worker.IdCardNumber;
            Birthday = worker.Birthday;
            NativePlace = worker.NativePlace;
            HealthStatus = worker.HealthStatus;
            QQ = worker.QQ;
            Wechat = worker.Wechat;
            Alipay = worker.Alipay;
            Enabled = worker.Enabled;
            IsHeader = worker.IsHeader;
            IsCandidate = worker.IsCandidate;
            HeaderId = worker.HeaderId;
            BestJobClassId = worker.BestJobClassId;

            if (worker.Header != null)
            {
                HeaderName = worker.Header.Name;
            }

            if (worker.BestJobClass != null)
            {
                BestJobClassName = worker.BestJobClass.Name;
            }

            if (worker.EndUserDistricts != null)
            {
                Districts = worker.EndUserDistricts.Select(e => new DistrictViewModel(e.District)).ToList();
            }

            if (worker.EndUserJobClasses != null)
            {
                JobClasses = worker.EndUserJobClasses.Select(e => new JobClassViewModel(e)).OrderBy(e => e.JobClassId).ToList();
            }

            if (worker.Workers != null)
            {
                Workers = worker.Workers.Select(w => new WorkerViewModel(w)).ToList();
            }

        }

        public Worker ToWorker()
        {
            Worker worker = new Worker
            {
                UserName = UserName,
                Portrait = Portrait,
                Name = Name,
                Gender = Gender,
                IdCardNumber = IdCardNumber,
                Birthday = Birthday,
                NativePlace = NativePlace,
                HealthStatus = HealthStatus,
                QQ = QQ,
                Wechat = Wechat,
                Alipay = Alipay,
                Enabled = Enabled,
                IsHeader = IsHeader,
                IsCandidate = IsCandidate,
                HeaderId = HeaderId,
                BestJobClassId = BestJobClassId,
            };

            // 确保班长自身不再有班长
            if (worker.IsHeader)
                worker.HeaderId = null;

            if (Id != null)
                worker.Id = Id;

            return worker;
        }

        public void Update(ref Worker worker)
        {
            worker.UserName = UserName;
            worker.Portrait = Portrait;
            worker.Name = Name;
            worker.Gender = Gender;
            worker.IdCardNumber = IdCardNumber;
            worker.Birthday = Birthday;
            worker.NativePlace = NativePlace;
            worker.HealthStatus = HealthStatus;
            worker.QQ = QQ;
            worker.Wechat = Wechat;
            worker.Alipay = Alipay;
            worker.Enabled = Enabled;
            worker.IsHeader = IsHeader;
            worker.IsCandidate = IsCandidate;
            worker.HeaderId = HeaderId;
            worker.BestJobClassId = BestJobClassId;
        }
    }
}
