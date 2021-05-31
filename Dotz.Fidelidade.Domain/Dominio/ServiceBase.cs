using Flunt.Notifications;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Dotz.Fidelidade.Domain.Dominio
{
    public class ServiceBase
    {
        public List<Notification> Notificacoes { get; protected set; } = new List<Notification>();

        protected void LimparErros()
        {
            Notificacoes = new List<Notification>();
        }

        protected void AdicionarNotificacao(Notification notificacao)
        {
            Notificacoes.Add(notificacao);
        }

        protected void AdicionarNotificacoes(IEnumerable<Notification> notificacoes)
        {
            Notificacoes.AddRange(notificacoes);
        }

        protected bool ValidarRequest<T>(T request, bool limparErros = true) where T : Notifiable, IValidatable
        {
            if (limparErros)
                LimparErros();

            Validar(request);

            return request.Valid;
        }

        protected bool ValidarRequests<T>(IEnumerable<T> requests, bool limparErros = true) where T : Notifiable, IValidatable
        {
            if (limparErros)
                LimparErros();

            if (!requests.Any())
                return false;

            var validationsRequest = new List<bool>();

            foreach (var request in requests)
            {
                Validar(request);

                validationsRequest.Add(request.Valid);
            }

            var valido = !validationsRequest.Any(x => x == false);

            return valido;
        }

        private void Validar<T>(T request) where T : Notifiable, IValidatable
        {
            request.Validate();

            if (request.Invalid)
                AdicionarNotificacoes(request.Notifications);
        }

        protected void AdicionarNotificacao(string campo, string mensagem)
        {
            AdicionarNotificacao(new Notification(campo, mensagem));
        }

        public bool Valido => Notificacoes.Count == 0;
        public bool Invalido => !Valido;
    }
}
