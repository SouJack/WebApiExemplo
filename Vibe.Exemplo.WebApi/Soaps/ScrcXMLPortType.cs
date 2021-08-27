using System.Threading.Tasks;

namespace Vibe.Exemplo.WebApi.Soaps
{
    public class ScrcXMLPortType: wsScrcXMLPortType
    {
        public XMLResponse XML(XMLRequest request)
        {
            var r = new XMLResponse("", "", "Nao", "zzzzzzzzzzz", "123");
            return r;
        }

        public XMLAreaLivreResponse XMLAreaLivre(XMLAreaLivreRequest request)
        {
            var r = new XMLAreaLivreResponse("", "", "Sim", "aaaaaaaaaaaaa", "321");
            return r;
        }

        public monitorResponse monitor(monitorRequest request)
        {
            var r = new monitorResponse("10.0.0.1");
            return r;
        }
    }
}