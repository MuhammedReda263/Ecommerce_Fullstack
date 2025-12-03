using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Sharing
{
    public class EmailStringBody
    {
        public static string Send(string email, string token, string component, string message)
        {
            string encodedToken = Uri.EscapeDataString(token);

            return $@"
<html>
<head>
    <style>
        body {{
            font-family: 'Arial', sans-serif;
            background: #0d1117; /* Dark background */
            margin: 0;
            padding: 0;
            color: #c9d1d9;
        }}

        .container {{
            max-width: 600px;
            margin: 40px auto;
            background: #161b22;
            padding: 35px;
            border-radius: 12px;
            box-shadow: 0 4px 25px rgba(0, 0, 0, 0.4);
            text-align: center;
        }}

        h1 {{
            color: #fff;
            font-size: 26px;
            margin-bottom: 15px;
        }}

        p {{
            color: #b1bac4;
            font-size: 16px;
            line-height: 1.7;
        }}

        .button {{
            display: inline-block;
            margin-top: 30px;
            padding: 15px 40px;
            font-size: 18px;
            font-weight: bold;
            color: #fff !important;
            background: linear-gradient(45deg, #238636, #2ea043); 
            border-radius: 10px;
            text-decoration: none;
            transition: 0.3s;
        }}

        .button:hover {{
            filter: brightness(1.15);
        }}

        .footer {{
            margin-top: 35px;
            font-size: 14px;
            color: #8b949e;
        }}

        hr {{
            border: 0;
            height: 1px;
            background: #30363d;
            margin: 25px 0;
        }}
    </style>
</head>

<body>
    <div class=""container"">
        <h1>{message}</h1>
        <hr>

        <p>Please click the button below to continue.</p>

        <a class=""button""
           href=""http://localhost:4200/account/{component}?email={email}&code={encodedToken}"">
            {message}
        </a>

        <p class=""footer"">
            If you didn’t request this action, simply ignore this email.<br>
            © {DateTime.Now.Year} Your Company — All rights reserved.
        </p>
    </div>
</body>
</html>";
        }


    }

}
