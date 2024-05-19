namespace BaseProject.Domain.Constants
{
    public static class EmailConstants
    {
        public static string BodyActivationEmail(string email) =>
            @"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Password Reset</title>
    <style>
        /* Reset styles */
        body, html {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            line-height: 1.6;
        }
        /* Container styles */
        .container {
            max-width: 600px;
            margin: 20px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 10px;
            background-color: #f9f9f9;
        }
        /* Heading styles */
        h1 {
            font-size: 24px;
            text-align: center;
            color: #333;
        }
        /* Paragraph styles */
        p {
            margin-bottom: 20px;
            color: #666;
        }
        /* Button styles */
        .btn {
            display: inline-block;
            padding: 10px 20px;
            background-color: #007bff;
            color: #fff;
            text-decoration: none;
            border-radius: 5px;
        }
        /* Footer styles */
        .footer {
            margin-top: 20px;
            text-align: center;
            color: #999;
        }
    </style>
</head>
<body>
    <div class=""container"">
        <p>Hello,</p>
        <p>Welcome to Base Project. Thank you for using our servicesđe</p>
        <p>To experience the service, please activate your account. Click the button below:</p>
        <p><a href=""http://localhost:5016/Home/Resetpassword?userId=2}"" class=""btn"">Active Account</a></p>
        <p>If you have any questions or need assistance, please contact our support team.</p>
        <p>Thank you,</p>
        <p>The Support Team</p>
        <div class=""footer"">
            <p>This is an automated message. Please do not reply.</p>
        </div>
    </div>
</body>
</html>
";

        public static string BodyResetPasswordEmail(string email) =>
            @"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Password Reset</title>
    <style>
        /* Reset styles */
        body, html {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            line-height: 1.6;
        }
        /* Container styles */
        .container {
            max-width: 600px;
            margin: 20px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 10px;
            background-color: #f9f9f9;
        }
        /* Heading styles */
        h1 {
            font-size: 24px;
            text-align: center;
            color: #333;
        }
        /* Paragraph styles */
        p {
            margin-bottom: 20px;
            color: #666;
        }
        /* Button styles */
        .btn {
            display: inline-block;
            padding: 10px 20px;
            background-color: #007bff;
            color: #fff;
            text-decoration: none;
            border-radius: 5px;
        }
        /* Footer styles */
        .footer {
            margin-top: 20px;
            text-align: center;
            color: #999;
        }
    </style>
</head>
<body>
    <div class=""container"">
        <p>Hello,</p>
        <p>We received a request to reset your password. If you did not make this request, you can ignore this email.</p>
        <p>To reset your password, please click the button below:</p>
        <p><a href=""http://localhost:5016/Home/Resetpassword?userId=2}"" class=""btn"">Reset Password</a></p>
        <p>If you have any questions or need assistance, please contact our support team.</p>
        <p>Thank you,</p>
        <p>The Support Team</p>
        <div class=""footer"">
            <p>This is an automated message. Please do not reply.</p>
        </div>
    </div>
</body>
</html>
";

        public const string SUBJECT_RESET_PASSWORD = "Base Project-Password Reset";
        public const string SUBJECT_ACTIVE_ACCOUNT = "Base Project-Active Account";
    }
}