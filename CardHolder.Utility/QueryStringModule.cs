using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;




/// <summary>
/// Summary description for QueryStringModule
/// </summary>
/// <remarks></remarks>
public class QueryStringModule : IHttpModule
{
    #region IHttpModule Members


    /// <summary>
    /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
    /// </summary>
    /// <remarks></remarks>
    public void Dispose()
    {
        // Nothing to dispose
    }


    /// <summary>
    /// Initializes a module and prepares it to handle requests.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
    /// <remarks></remarks>
    public void Init(HttpApplication context)
    {
        //context.BeginRequest += new EventHandler(context_BeginRequest);
        //context.EndRequest += new EventHandler(context_EndRequest);
    }


    #endregion


    /// <summary>
    /// 
    /// </summary>
    private const string PARAMETER_NAME = "enc=";
    /// <summary>
    /// 
    /// </summary>
    private const string ENCRYPTION_KEY = "key";


    /// <summary>
    /// Handles the BeginRequest event of the context control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    void context_BeginRequest(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
        string query = string.Empty;
        string path = string.Empty;


        try
        {
            if (context.Request.Url.OriginalString.Contains("aspx") && context.Request.RawUrl.Contains("?"))
            {
                query = ExtractQuery(context.Request.RawUrl);
                path = GetVirtualPath();


                if (query.StartsWith(PARAMETER_NAME, StringComparison.OrdinalIgnoreCase))
                {
                    // Decrypts the query string and rewrites the path.
                    string rawQuery = query.Replace(PARAMETER_NAME, string.Empty);
                    string decryptedQuery = Decrypt(rawQuery);
                    context.RewritePath(path, string.Empty, decryptedQuery);
                }
                else //if (context.Request.HttpMethod == "GET")
                {
                    // Encrypt the query string and redirects to the encrypted URL.
                    // Remove if you don't want all query strings to be encrypted automatically.
                    string encryptedQuery = Encrypt(query);
                    context.Response.Redirect(path + encryptedQuery, false);
                }
                //else if (context.Request.HttpMethod == "POST")
                //{

                //    string encryptedQuery = Encrypt(query);
                //    context.Response.Redirect(path + encryptedQuery, false);
                //}
            }
        }
        catch (Exception)
        {
            // m_Logger.Error("An error occurred while parsing the query string in the URL: " + path, ex);
            context.Response.Redirect("~/Login.aspx");
        }


    }


    //void context_EndRequest(object sender, EventArgs e)
    //{
    //}

    /// <summary>
    /// Parses the current URL and extracts the virtual path without query string.
    /// </summary>
    /// <returns>The virtual path of the current URL.</returns>
    /// <remarks></remarks>
    private static string GetVirtualPath()
    {
        string path = HttpContext.Current.Request.RawUrl;
        path = path.Substring(0, path.IndexOf("?"));
        path = path.Substring(path.LastIndexOf("/") + 1);
        return path;
    }


    /// <summary>
    /// Parses a URL and returns the query string.
    /// </summary>
    /// <param name="url">The URL to parse.</param>
    /// <returns>The query string without the question mark.</returns>
    /// <remarks></remarks>
    private static string ExtractQuery(string url)
    {
        int index = url.IndexOf("?") + 1;
        return url.Substring(index);
    }


    #region Encryption/decryption


    /// <summary>
    /// The salt value used to strengthen the encryption.
    /// </summary>
    private readonly static byte[] SALT = Encoding.ASCII.GetBytes(ENCRYPTION_KEY.Length.ToString());


    /// <summary>
    /// Encrypts any string using the Rijndael algorithm.
    /// </summary>
    /// <param name="inputText">The string to encrypt.</param>
    /// <returns>A Base64 encrypted string.</returns>
    /// <remarks></remarks>
    public static string Encrypt(string inputText)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] plainText = Encoding.Unicode.GetBytes(inputText);
        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);


        using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16)))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainText, 0, plainText.Length);
                    cryptoStream.FlushFinalBlock();
                    return "?" + PARAMETER_NAME + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
    }


    /// <summary>
    /// Decrypts a previously encrypted string.
    /// </summary>
    /// <param name="inputText">The encrypted string to decrypt.</param>
    /// <returns>A decrypted string.</returns>
    /// <remarks></remarks>
    public static string Decrypt(string inputText)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();


        byte[] encryptedData = Convert.FromBase64String(inputText);
        PasswordDeriveBytes secretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);


        using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
        {
            using (MemoryStream memoryStream = new MemoryStream(encryptedData))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    byte[] plainText = new byte[encryptedData.Length];
                    int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                    return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                }
            }
        }
    }


    #endregion


}