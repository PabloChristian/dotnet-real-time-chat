﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Real.Time.Chat.Domain.Properties {
    using System;
    
    
    /// <summary>
    ///   Uma classe de recurso de tipo de alta segurança, para pesquisar cadeias de caracteres localizadas etc.
    /// </summary>
    // Essa classe foi gerada automaticamente pela classe StronglyTypedResourceBuilder
    // através de uma ferramenta como ResGen ou Visual Studio.
    // Para adicionar ou remover um associado, edite o arquivo .ResX e execute ResGen novamente
    // com a opção /str, ou recrie o projeto do VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public Resources() {
        }
        
        /// <summary>
        ///   Retorna a instância de ResourceManager armazenada em cache usada por essa classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Real.Time.Chat.Domain.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Substitui a propriedade CurrentUICulture do thread atual para todas as
        ///   pesquisas de recursos que usam essa classe de recurso de tipo de alta segurança.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Message was not delivered. please, select an user.
        /// </summary>
        public static string Message_NotDelivered_SelectUser {
            get {
                return ResourceManager.GetString("Message_NotDelivered_SelectUser", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The message is required..
        /// </summary>
        public static string Message_Required {
            get {
                return ResourceManager.GetString("Message_Required", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The sender is required..
        /// </summary>
        public static string Message_Sender_Required {
            get {
                return ResourceManager.GetString("Message_Sender_Required", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a User already exists.
        /// </summary>
        public static string User_AlreadyExists {
            get {
                return ResourceManager.GetString("User_AlreadyExists", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The name is required..
        /// </summary>
        public static string User_Name_Required {
            get {
                return ResourceManager.GetString("User_Name_Required", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a User not found.
        /// </summary>
        public static string User_NotFound {
            get {
                return ResourceManager.GetString("User_NotFound", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The password must have minimum of 6 characters.
        /// </summary>
        public static string User_Password_MinimumLength {
            get {
                return ResourceManager.GetString("User_Password_MinimumLength", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The passwords are not equal.
        /// </summary>
        public static string User_Password_NotEqual {
            get {
                return ResourceManager.GetString("User_Password_NotEqual", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The password is required.
        /// </summary>
        public static string User_Password_Required {
            get {
                return ResourceManager.GetString("User_Password_Required", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Repeat the password.
        /// </summary>
        public static string User_RepeatPassword_Required {
            get {
                return ResourceManager.GetString("User_RepeatPassword_Required", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The username is required..
        /// </summary>
        internal static string User_UserName_Required {
            get {
                return ResourceManager.GetString("User_UserName_Required", resourceCulture);
            }
        }
    }
}
