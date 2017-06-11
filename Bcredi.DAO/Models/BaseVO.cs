using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bcredi.DAO.Models
{
    [DataContract]
    public class BaseVO
    {
        /// <summary>
        /// id do objeto
        /// </summary>        
        [DisplayName("Id")]
        private int id { get; set; }

        /// <summary>
        /// guid do objeto
        /// </summary>        
        [DisplayName("guid")]
        public Guid Guid { get; set; }

        [DataMember]
        public int Id { get { return id; } set { id = value; } }

        /// <summary>
        /// Descricao do objeto
        /// </summary>        
        [DisplayName("Descri&ccedil;&atilde;o")]
        private string descricao { get; set; }

        [DataMember]
        public string Descricao { get { return descricao; } set { descricao = value; } }

        /// <summary>
        /// Representa se o objeto esta ativo
        /// </summary>        
        private bool isAtivo;

        [DisplayName("Ativo")]
        [DataMember]
        public bool IsAtivo { get { return isAtivo; } set { isAtivo = value; } }

        [DataMember]
        /// <summary>
        /// Data de criacao do objeto
        /// </summary>        
        private DateTime dataCriacao;

        [DataMember]
        [DisplayName("Data de Criação")]
        public DateTime DataCriacao { get { return dataCriacao; } set { dataCriacao = value; } }

        /// <summary>
        /// Data de atualizacao do objeto
        /// </summary>
        private DateTime dataAtualizacao;

        [DisplayName("Data de Atualização")]
        public DateTime DataAtualizacao { get { return dataAtualizacao; } set { dataAtualizacao = value; } }

        /// <summary>
        /// Criado do objeto
        /// </summary>
        private Usuario usuarioCriador;

        [DisplayName("Usuário Inserção")]
        public Usuario UsuarioCriador { get { return usuarioCriador; } set { usuarioCriador = value; } }

        /// <summary>
        /// Atualizado do objeto
        /// </summary>
        private Usuario usuarioAtualizador;

        [DisplayName("Usuário Atualização")]
        public Usuario UsuarioAtualizador { get { return usuarioAtualizador; } set { usuarioAtualizador = value; } }

        /// <summary>
        /// Recuperar o id
        /// </summary>
        /// <returns></returns>
        public int getId()
        {
            return id;
        }

        /// <summary>
        /// Recupera a descricao
        /// </summary>
        /// <returns></returns>
        public string getDescricao()
        {
            return descricao;
        }

        /// <summary>
        /// Recupera o status
        /// </summary>
        /// <returns></returns>
        public bool getAtivo()
        {
            return isAtivo;
        }

        /// <summary>
        /// Recupera a data de criacao
        /// </summary>
        /// <returns></returns>
        public DateTime getDataCriacao()
        {
            return dataCriacao;
        }

        /// <summary>
        /// Recupera a data de atualizacao. Quando estiver nula, o objeto nunca foi atualizado
        /// </summary>
        /// <returns></returns>
        public DateTime getDataAtualizacao()
        {
            return dataAtualizacao;
        }

        /// <summary>
        /// Recupera o usuário que criou o registro
        /// </summary>
        /// <returns></returns>
        public Usuario getUsuarioCriador()
        {
            return usuarioCriador;
        }

        /// <summary>
        /// Recupera o usuário que atualizou o registro
        /// </summary>
        /// <returns></returns>
        public Usuario getUsuarioAtualizador()
        {
            return usuarioAtualizador;
        }

        /// <summary>
        /// Configura o atributo id
        /// </summary>
        /// <param name="id"></param>
        public void setId(int id)
        {
            Id = id;
            this.id = id;
        }

        /// <summary>
        /// Configura o atributo descricao
        /// </summary>
        /// <param name="descricao"></param>
        public void setDescricao(string descricao)
        {
            Descricao = descricao;
            this.descricao = descricao;
        }

        /// <summary>
        /// Configura o status
        /// </summary>
        /// <param name="isAtivo"></param>
        public void setAtivo(bool isAtivo)
        {
            IsAtivo = IsAtivo;
            this.isAtivo = isAtivo;
        }

        /// <summary>
        /// Configura a data de criacao
        /// </summary>
        /// <param name="dataCriacao"></param>
        public void setDataCriacao(DateTime dataCriacao)
        {
            DataCriacao = dataCriacao;
            this.dataCriacao = dataCriacao;
        }

        /// <summary>
        /// Configura a data de atualizacao
        /// </summary>
        /// <param name="dataAtualizacao"></param>
        public void setDataAtualizacao(DateTime dataAtualizacao)
        {
            DataAtualizacao = dataAtualizacao;
            this.dataAtualizacao = dataAtualizacao;
        }

        /// <summary>
        /// Configura o usuário que criou o registro
        /// </summary>
        /// <param name="usuarioCriador"></param>
        public void setUsuarioCriador(Usuario usuarioCriador)
        {
            UsuarioCriador = usuarioCriador;
            this.usuarioCriador = usuarioCriador;
        }

        /// <summary>
        /// Configura o usuário que atualizou o registro
        /// </summary>
        /// <param name="usuarioAtualizador"></param>
        public void setUsuarioAtualizador(Usuario usuarioAtualizador)
        {
            UsuarioAtualizador = usuarioAtualizador;
            this.usuarioAtualizador = usuarioAtualizador;
        }

        /// <summary>
        /// Recupera uma nova instância da classe
        /// </summary>
        /// <returns></returns>
        public BaseVO getInstance()
        {
            return new BaseVO();
        }

        /// <summary>
        /// Configura um novo objeto
        /// </summary>
        /// <param name="id">Id do objeto</param>
        /// <param name="descricao">Descricao do objeto</param>
        /// <param name="isAtivo">Status do registro</param>
        /// <param name="idUsuarioInsert">id do usuário que inseriu</param>
        /// <param name="idUsuarioUpdate">id do usuário que atualizou</param>
        /// <returns>Objeto configurado</returns>
        public BaseVO build(int id, string descricao, bool isAtivo, int idUsuarioInsert, int idUsuarioUpdate)
        {

            BaseVO objeto = new BaseVO();

            Usuario usuarioInsert = new Usuario();
            usuarioInsert.setId(idUsuarioInsert);

            Usuario usuarioUpdate = new Usuario();
            usuarioInsert.setId(idUsuarioUpdate);

            objeto.setId(id);
            objeto.setDescricao(descricao);
            objeto.setAtivo(isAtivo);

            return objeto;
        }

        /// <summary>
        /// Configura um novo objeto para insercao
        /// </summary>
        /// <param name="id">Id do objeto</param>
        /// <param name="descricao">Descricao do objeto</param>
        /// <param name="isAtivo">Status do registro</param>
        /// <param name="idUsuarioInsert">id do usuário que inseriu</param>        
        /// <returns>Objeto configurado</returns>
        public BaseVO buildInsert(int id, string descricao, bool isAtivo, int idUsuarioInsert)
        {
            BaseVO objeto = new BaseVO();

            Usuario usuarioInsert = new Usuario();
            usuarioInsert.setId(idUsuarioInsert);

            objeto.setId(id);
            objeto.setDescricao(descricao);
            objeto.setAtivo(isAtivo);
            objeto.setUsuarioCriador(usuarioInsert);

            return objeto;
        }

        /// <summary>
        /// Configura um novo objeto para atualizacao
        /// </summary>
        /// <param name="id">Id do objeto</param>
        /// <param name="descricao">Descricao do objeto</param>
        /// <param name="isAtivo">Status do registro</param>
        /// <param name="idUsuarioUpdate">id do usuário que atualizou</param>
        /// <returns>Objeto configurado</returns>
        public BaseVO buildUpdate(int id, string descricao, bool isAtivo, int idUsuarioUpdate)
        {
            BaseVO objeto = new BaseVO();

            Usuario usuarioUpdate = new Usuario();
            usuarioUpdate.setId(idUsuarioUpdate);

            objeto.setId(id);
            objeto.setDescricao(descricao);
            objeto.setAtivo(isAtivo);
            objeto.setUsuarioAtualizador(usuarioUpdate);

            return objeto;
        }

        /*
        TODO Marcelo: Utilizar reflection
        public void setPropertyValue(this object obj, string propName, object value)
        {
            obj.GetType().GetProperty(propName).SetValue(obj, value, null);
        }

        public object GetPropertyValue(this object obj, string propName)
        {
            return obj.GetType().GetProperty(propName).GetValue(obj, null);
        }

        
        public object[] getAllAtributes(BaseVO classe) {
            
            System.Reflection.MemberInfo info = typeof(classe);
            object[] attributes = info.GetCustomAttributes(true);
            for (int i = 0; i < attributes.Length; i++)
            {
                System.Console.WriteLine(attributes[i]);
            }

            return attributes;
        }
        */
        public override string ToString()
        {
            var flags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.FlattenHierarchy;
            System.Reflection.PropertyInfo[] infos = this.GetType().GetProperties(flags);

            StringBuilder sb = new StringBuilder();

            string typeName = this.GetType().Name;
            sb.AppendLine(typeName);
            sb.AppendLine(string.Empty.PadRight(typeName.Length + 5, '='));

            foreach (var info in infos)
            {
                object value = info.GetValue(this, null);
                //TODO Melhorar para nao usar NewLine e alterar para o caracter "|"
                sb.AppendFormat("{0}: {1}{2}", info.Name, value != null ? value : "null", Environment.NewLine);
            }

            return sb.ToString();
        }


        /// <summary>
        /// Observacao.
        /// </summary>
        [DisplayName("Observacao")]
        [DataMember]
        public string Observacao { get; set; }
    }
}
