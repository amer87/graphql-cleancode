using System.Threading.Tasks;

namespace Com.Application.Common.Interfaces;

public interface IPDFService
{

    /// <summary>
    /// This function is to generate pdf 
    /// </summary>
    /// <typeparam name="T">Any data </typeparam>
    /// <param name="data">The content in form of header and lines</param>
    /// <param name="docName">Physical PDF file name</param>
    /// <returns></returns>
    
    Task Create<T>(T data, string docName);
    /// <summary>
    /// This function is to generate pdf 
    /// </summary>
    /// <typeparam name="T">Any data </typeparam>
    /// <param name="data">The content in form of header and lines</param>
    /// <param name="docName">Physical PDF file name</param>
    /// <param name="template">The template name</param>
    /// <param name="storagePath">Physical storage path</param>
    /// <returns></returns>
    Task Create<T>(T data, string docName, string template, string storagePath);
}
