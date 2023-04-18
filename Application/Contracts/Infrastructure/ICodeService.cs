using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Infrastructure
{
    public interface ICodeService
    {
        string Generate(string requestUsername, string requestTelephone, out bool unknown);
        bool VerifyCode(string username, string inputPhoneNumber, string inputConfirmationCode);
        void ClearCache();
    }
}
