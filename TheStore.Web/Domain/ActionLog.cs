using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStore.Web.Domain
{
    [Table("ActionLogs")]
    public class ActionLog
    {
        public ActionLog(ApplicationUser user, string action, string controller, string description)
        {
            PerformedAt = DateTime.UtcNow;
            PerformedBy = user;
            Action = action;
            Controller = controller;
            Description = description;
        }
        [Key]
        public int Id { get; set; }
        public DateTime PerformedAt { get; set; }
        [StringLength(255)]
        public string Action { get; set; }
        [StringLength(255)]
        public string Controller { get; set; }
        public string Description { get; set; }
        public int PerformedById { get; set; }
        public virtual ApplicationUser PerformedBy { get; set; }
    }
}