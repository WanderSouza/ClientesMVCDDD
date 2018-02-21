using System.Web;
using System.Web.Optimization;

namespace ClientesMVC.MVC
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //Registra o plugin para validação via jquery
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            //Registra o plugin para criação de máscaras com jquery
            bundles.Add(new ScriptBundle("~/bundles/jquerymask").Include(
                        "~/Scripts/jquery.maskedinput.js"));

            //Registra o javascript dos métodos customizados do projeto
            bundles.Add(new ScriptBundle("~/bundles/clientesCustomMethods").Include(
                        "~/Scripts/Clientes/clientesCustomMethods.js"));

            //Registra o javascript dos métodos utilizados nas views Edit, Details e Delete do projeto
            bundles.Add(new ScriptBundle("~/bundles/clientesViewMethods").Include(
                        "~/Scripts/Clientes/clientesViewMethods.js"));

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            // pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));
        }
    }
}
