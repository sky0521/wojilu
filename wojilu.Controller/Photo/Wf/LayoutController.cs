﻿using System;
using System.Collections.Generic;
using System.Text;
using wojilu.Web.Mvc;
using wojilu.Apps.Photo.Domain;
using wojilu.Apps.Photo.Interface;
using wojilu.Apps.Photo.Service;
using wojilu.Members.Users.Domain;

namespace wojilu.Web.Controller.Photo.Wf {

    public class LayoutController : ControllerBase {

        public IPhotoSysCategoryService sysCategoryService { get; set; }

        public LayoutController() {
            sysCategoryService = new PhotoSysCategoryService();
        }

        public override void Layout() {

            set( "lnkHome", PhotoLink.ToHome() );
            set( "lnkFollowing", to( new HomeController().Following ) );
            set( "lnkHot", PhotoLink.ToHot() );
            set( "lnkPick", PhotoLink.ToPick() );

            set( "lnkAdd", to( new HomeController().Add ) );

            bindCategories();
            bindAdminCmd();
        }

        private void bindCategories() {
            List<PhotoSysCategory> categories = sysCategoryService.GetAll();
            IBlock cblock = getBlock( "categories" );
            foreach (PhotoSysCategory x in categories) {

                cblock.Set( "x.Name", x.Name );
                cblock.Set( "x.LinkShow", PhotoLink.ToCategory( x.Id ) );
                cblock.Next();
            }
        }

        private void bindAdminCmd() {

            String adminCmd = "";

            if (ctx.viewer.IsLogin) {

                User u = ctx.viewer.obj as User;
                adminCmd = string.Format( "<a href='{0}' target='_blank'><i class='icon-picture'></i> 管理图片</a> <a href='{1}' class='left20' target='_blank'><i class='icon-th'></i> 管理专辑</a>", PhotoLink.ToAdminPost( u ), PhotoLink.ToAdminAlbum( u ) );
            }

            set( "adminCmd", adminCmd );
        }

    }

}
