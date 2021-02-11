<%@ Application Language="C#" %>
<%@ Import Namespace = "Microsoft.IdentityModel.Web" %>
<%@ Import Namespace = "Microsoft.IdentityModel.Tokens.Saml11" %>
<%@ Import Namespace = "Microsoft.IdentityModel.Tokens.Saml2" %>
<%@ Import Namespace = "System.IdentityModel.Tokens" %>
<%@ Import Namespace = "System.IdentityModel.Selectors" %>
<%@ Import Namespace = "System.IdentityModel.Claims" %>
<%@ Import Namespace = "System.IdentityModel.Policy" %>
<%@ Import Namespace = "System.ServiceModel" %>
<%@ Import Namespace = "System.Security.Cryptography.X509Certificates" %>
<%@ Import Namespace = "System.ServiceModel.Security" %>
<%@ Import Namespace = "System.ComponentModel" %>
<%@ Import Namespace = "System.Diagnostics" %>

<script runat="server">

    //public override void Init()
    //{
    //    base.Init();
    //    FederatedAuthenticationModule fam = FederatedAuthenticationModule.Current;
    //    fam.ConfigurationLoaded += new EventHandler(FAM_ConfigurationLoaded);
    //}

    //void FAM_ConfigurationLoaded(object sender, EventArgs e)
    //{
    //    FederatedAuthenticationModule m = (FederatedAuthenticationModule)sender;
    //    m.TokenHandlers.Remove(m.TokenHandlers[typeof(Saml2SecurityToken)]);
    //    Saml11TokenHandler handler = m.TokenHandlers[typeof(SamlSecurityToken)] as Saml11TokenHandler;
    //    handler.SamlSecurityTokenRequirement.IssuerTokenAuthenticators.Clear();
    //    handler.SamlSecurityTokenRequirement.IssuerTokenAuthenticators.Add(
    //        new X509SecurityTokenAuthenticator(
    //            new CustomX509CertificateValidator(
    //                ConfigurationManager.AppSettings["CNSign"].ToString()
    //            )
    //        )
    //    );
    //}

    //void FederatedAuthentication_PreFederatedAuthenticate(object sender, CancelEventArgs e)
    //{
    //    if (HttpContext.Current.Request.Path.EndsWith("Home.aspx"))
    //    {
    //        e.Cancel = true;
    //    }
    //}

    //public class CustomX509CertificateValidator : X509CertificateValidator
    //{
    //    X509Certificate2 _issuerCert;
    //    public CustomX509CertificateValidator(string subjectName)
    //    {
    //        _issuerCert = CertificateUtil.GetCertificate(StoreName.My, StoreLocation.LocalMachine, subjectName);
    //        if (_issuerCert == null)
    //        {
    //            throw new ArgumentException(String.Format("No se puede encontrar el certificado con el asunto {0}", subjectName));
    //        }
    //    }
    //    public override void Validate(System.Security.Cryptography.X509Certificates.X509Certificate2 certificate)
    //    {
    //        if (certificate.Thumbprint != _issuerCert.Thumbprint)
    //        {
    //            throw new SecurityTokenException("Validación fallida del certificado emitido");
    //        }
    //    }
    //}

    //void FederatedAuthentication_ValidatingTicket(object sender, ValidatingTicketEventArgs e)
    //{
    //    DateTime now = DateTime.UtcNow;
    //    TimeSpan window = new TimeSpan(0, int.Parse(ConfigurationSettings.AppSettings["windowMinutes"]), 0);

    //    DateTime wouldExpire = now + new TimeSpan(0, int.Parse(ConfigurationSettings.AppSettings["wouldExpireMinutes"]), 0);
    //    DateTime maxExpiration = e.Ticket.CreationTime + new TimeSpan(0, int.Parse(ConfigurationSettings.AppSettings["maxExpirationMinutes"]), 0);

    //    // If this request comes within 10 seconds of expiration
    //    // and the session hasn't been active longer than 1 minute,
    //    // extend the session 20 seconds.
    //    if (now < e.Ticket.ExpirationTime &&
    //        e.Ticket.ExpirationTime - now <= window &&
    //        e.Ticket.ExpirationTime < maxExpiration)
    //    {
    //        e.ExtendedExpirationTime = (wouldExpire < maxExpiration)
    //                                 ? wouldExpire
    //                                 : maxExpiration;
    //    }
    //}

    //protected void Application_Error(object sender, EventArgs e)
    //{
    //    ExceptionPolicyWrapper.HandleException(Server.GetLastError().GetBaseException());

    //    //Exception objErr = Server.GetLastError().GetBaseException();
    //    //string err = "Error Caught in Application_Error event\n" +
    //    //             "Error in: " + Request.Url.ToString() +
    //    //             "\nError Message:" + objErr.Message.ToString() +
    //    //             "\nStack Trace:" + objErr.StackTrace.ToString();
    //    //EventLog.WriteEntry("Sample_WebApp", err, EventLogEntryType.Error);
    //} 
    
</script>
