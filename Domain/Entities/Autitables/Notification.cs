using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Autitables
{
    public class Notification : AuditableEntity
    {
        public NotificationType Type { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public IEnumerable<UserNotification> UserNotifications { get; set; }


        private Notification()
        {

        }

        private Notification(string value, string description, NotificationType notificationType)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));
            Description = description;
            Type = notificationType;
            Created = DateTime.Now;
            Value = value;
        }

        public static Notification NewUserRegistered(string newUserName)
        {
            if (string.IsNullOrWhiteSpace(newUserName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(newUserName));
            return new Notification(newUserName,
                $"Zarejestrował się nowy użytkownik.", NotificationType.UserRegistered);
        }

        public static Notification NewReport(string user, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(user)) throw new ArgumentNullException(nameof(user));
            return new Notification(userId.ToString(), $"Pojawiło się nowe zgłoszenie od użytkownika {user}", NotificationType.NewReport);
        }
        public static Notification AdminRepliedToReport(Guid reportId)
        {
            return new Notification(reportId.ToString(), $"Pojawiła się odpowiedź na twoje zgłoszenie", NotificationType.ReportAnswear);
        }
        public static Notification UserRepliedToOpenedReport(Guid reportId)
        {
            return new Notification(reportId.ToString(), $"Pojawiła się odpowiedź na rozpoczęte zgłoszenie użytkownika.", NotificationType.ReportAnswear);
        }

        public static Notification ReportClosed(Guid reportId)
        {
            return new Notification(reportId.ToString(), $"Twoje zgłoszenie zostało zamknięte.", NotificationType.ReportClosed);
        }
    }
    public enum NotificationType
    {
        UserRegistered,
        UserForgotPassword,
        NewReport,
        ReportAnswear,
        ReportClosed,
    }
    public class UserNotification
    {
        public Guid UserId { get; set; }
        public Guid NotificationId { get; set; }
        public Notification Notification { get; set; }
        public bool IsRead { get; set; }
        public Guid? ItemId { get; set; }

        public void Read()
        {
            IsRead = true;
            ReadDateTime = DateTime.Now;
        }
        public void NotRead()
        {
            IsRead = false;
            ReadDateTime = DateTime.MinValue;
        }

        public DateTime ReadDateTime { get; set; }

        /// <summary>
        /// Tworzy notyfikację rejestracji nowego użytkownika i przypisuje ją do użytkownika.
        /// </summary>
        /// <param name="adminId">Id admina do którego przypisać powiadomienie.</param>
        /// <param name="newUsername">WAR nowego uzytkownika.</param>
        /// <returns></returns>new usernotification
        public static UserNotification NewUserRegistered(Notification notification, Guid adminId)
        {
            return new UserNotification
            {
                UserId = adminId,
                IsRead = false,
                Notification = notification
            };
        }

        public static UserNotification NewReport(Notification notification, Guid adminId)
        {
            return new UserNotification
            {
                UserId = adminId,
                IsRead = false,
                Notification = notification
            };
        }
        public static UserNotification AdminRepliedToReport(Guid reportId, Guid userId)
        {
            return new UserNotification
            {
                UserId = userId,
                IsRead = false,
                Notification = Notification.AdminRepliedToReport(reportId)
            };
        }
        public static UserNotification UserRepliedToOpenedReport(Notification notification, Guid adminId)
        {
            return new UserNotification
            {
                UserId = adminId,
                IsRead = false,
                Notification = notification
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportId">Id which has been closed</param>
        /// <param name="userId">User to notify</param>
        /// <returns></returns>
        public static UserNotification ReportHasBeenClosed(Guid reportId, Guid userId)
        {
            return new UserNotification
            {
                UserId = userId,
                IsRead = false,
                Notification = Notification.ReportClosed(reportId)
            };
        }
    }

}
