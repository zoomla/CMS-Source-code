namespace ZoomLa.ZLEnum
{
    using System;

    public enum MailState
    {
        AttachmentSizeLimit = 10,
        ConfigFileIsWriteOnly = 6,
        FileNotFind = 3,
        MailConfigIsNullOrEmpty = 4,
        MustIssueStartTlsFirst = 11,
        NoMailToAddress = 1,
        None = 0,
        NonsupportSsl = 12,
        NoSubject = 2,
        Ok = 0x63,
        PortError = 13,
        SaveFailure = 7,
        SendFailure = 5,
        SmtpServerNotFind = 8,
        UserNameOrPasswordError = 9
    }
}
