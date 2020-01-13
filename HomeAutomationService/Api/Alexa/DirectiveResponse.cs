
namespace HomeAutomationService.Alexa 
{

    public class DirectiveResponse 
    {
        public Header header {get; set; }
        public Directive directive {  get; set; }
   
    }

    public class Directive
    {
        public string type { get; set; }
        public string speech { get; set; }
    }

    public class Header
    {
        public string requestId { get; set; }
    }
}
