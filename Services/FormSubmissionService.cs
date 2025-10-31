using Onatrix.ViewModels;
using Umbraco.Cms.Core.Services;

namespace Onatrix.Services;

public class FormSubmissionService(IContentService contentService)
{
    private readonly IContentService _contentService = contentService;

    public bool SaveCallbackRequest(FormViewModel model)
    {
        try
        {
            var container = _contentService.GetRootContent().FirstOrDefault(x => x.ContentType.Alias == "formSubmissions");
            if (container == null)
            {
                return false;
            }

            var requestName = $"{DateTime.Now:yyyy-MM-dd HH:mm} - {model.Name}";
            var request = _contentService.Create(requestName, container, "formRequest");

            request.SetValue("formRequestName", model.Name);
            request.SetValue("formRequestEmail", model.Email);
            request.SetValue("formRequestPhone", model.Phone);
            request.SetValue("formRequestOption", model.SelectedOption);

            var saveResult = _contentService.Save(request);
            return saveResult.Success;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
