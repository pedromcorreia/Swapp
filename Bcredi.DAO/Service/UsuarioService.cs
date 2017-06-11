using Bcredi.DAO.Models;
using Bcredi.DAO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bcredi.DAO.Service
{
    public class UsuarioService
    {
        //TODO Javier revisar este codigo
        UsuarioRepository usuarioRepository = new UsuarioRepository();

        public List<Usuario> getDescriptionPartial(string texto)
        {
            return usuarioRepository.getDescriptionPartial(texto);
        }

        public List<Usuario> getUsuariosByHierarquia(int idPerfil)
        {
            return usuarioRepository.getUsuariosByHierarquia(idPerfil);
        }

        public List<Usuario> getUsuariosBySuperiorHierarquico(int idSuperiorHierarquico)
        {
            return usuarioRepository.getUsuariosBySuperiorHierarquico(idSuperiorHierarquico);
        }

        public Usuario getUsuarioById(int idUsuario)
        {
            return usuarioRepository.getUsuarioById(idUsuario);
        }
        public Usuario getUsuarioAtivoById(int idUsuario, bool boolIsAtivo)
        {
            return usuarioRepository.getUsuarioAtivoById(idUsuario, boolIsAtivo);
        }

        public Usuario getUsuarioByEmail(string email)
        {
            return usuarioRepository.getUsuarioByEmail(email);
        }

        public int updateSenha(string login, string senha)
        {
            return usuarioRepository.updateSenha(login, senha);
        }

        public bool isValidDataExpiracaoSenha(string login)
        {
            return usuarioRepository.isValidDataExpiracaoSenha(login);
        }

        public bool IsLoginOrEmailExist(string login, string email)
        {
            return usuarioRepository.IsLoginOrEmailExist(login, email);
        }

        internal bool IsLoginOrEmailExist(Func<string, ActionResult> login, string email)
        {
            throw new NotImplementedException();
        }

        public int saveOrUpdate(Usuario usuario)
        {
            return usuarioRepository.saveOrUpdate(usuario);
        }

        public List<Usuario> getAll(bool isAtivo = true)
        {
            return usuarioRepository.getAll(isAtivo);
        }

        public Usuario setUsuarioSalvar(Usuario usuario)
        {
            return usuarioRepository.setUsuarioSalvar(usuario);
        }
        public Usuario getUsuarioByLoginSenha(string login, string password)
        {
            return usuarioRepository.getUsuarioByLoginSenha(login, password);
        }

        public void atualizarUltimoAcesso(int idUsuario, string ip)
        {
            usuarioRepository.atualizarUltimoAcesso(idUsuario, ip);
        }

        public Usuario getUsuarioByTokenResetSenha(string token)
        {
            return usuarioRepository.getUsuarioByTokenResetSenha(token);
        }
        public int iniciaProcessoUpdateSenha(int idUsuario, string token, string codSeguranca)
        {
            return usuarioRepository.iniciaProcessoTrocaDeSenha(idUsuario, token, codSeguranca);
        }

        public int AtivarCadastroUsuarioService(string login)
        {
            return usuarioRepository.AtivarCadastroUsuarioRepository(login);
        }

        public int getIdUsuarioByLogin(string login)
        {
            return usuarioRepository.getIdUsuarioByLogin(login);
        }

        public VM_UsuarioComplementoDadosBancarios DadosComplementaresGet(string guidUsuario)
        {
            return usuarioRepository.PesquisaUsuarioComplementoDadosBancarios(guidUsuario);
        }

        public bool AlterarSenhaUpdate(Usuario usuario)
        {
            return usuarioRepository.AlterarSenhaUpdate(usuario);
        }

        public bool InsertOrUpdateUsuarioComplementoDadosBancarios(VM_UsuarioComplementoDadosBancarios obj)
        {
            return usuarioRepository.InsertOrUpdateUsuarioComplementoDadosBancariosRepository(obj);
        }

        public bool UpdateUsuarioAssinatura(string guidUsuario, string guidDocumentoClickSign)
        {
            return usuarioRepository.UpdateUsuarioAssinatura(guidUsuario, guidDocumentoClickSign);
        }
    }
}
