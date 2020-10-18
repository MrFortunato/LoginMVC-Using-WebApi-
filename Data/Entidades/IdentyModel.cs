using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exame_Online.Data.Entidades
{
    public class IdentyModel
    {
        
            //O Identity é uma classe concreta que gerencia o usuário. 
            //É definido no espaço para nome Microsoft.Extensions.Identity.Core.
            //Esta classe cria, atualiza e exclui os usuários.
            //Possui métodos para localizar um usuário por ID do usuário, 
            //nome do usuário e email.
            //Ele também fornece a funcionalidade para adicionar declarações, 
            //remover declarações, adicionar e remover funções, etc.
            //Também gera hash de senha, valida usuários etc.
            public class Usuario : IdentityUser<int>
            {
                public virtual tb_DadosPessoais Pessoa { get; set; }
             
            }
            public class Perfil : IdentityRole<int> { }
            public class Claims : IdentityUserClaim<int> { }
            public class PerfilUsuario : IdentityUserRole<int> { }
            public class UsuarioLogin : IdentityUserLogin<int> { }
            public class UsuarioClaim : IdentityUserClaim<int> { }
            public class UsuarioToken : IdentityUserToken<int> { }
            public class PerfilClaim : IdentityRoleClaim<int> { }
        }
    
}
