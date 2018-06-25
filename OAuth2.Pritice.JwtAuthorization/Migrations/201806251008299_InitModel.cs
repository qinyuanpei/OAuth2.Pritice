namespace OAuth2.Pritice.JwtAuthorization.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppName = c.String(),
                        ClientId = c.String(),
                        ClientSecret = c.String(),
                        RedirectURL = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateStoredProcedure(
                "dbo.ClientModel_Insert",
                p => new
                    {
                        AppName = p.String(),
                        ClientId = p.String(),
                        ClientSecret = p.String(),
                        RedirectURL = p.String(),
                    },
                body:
                    @"INSERT [dbo].[ClientModels]([AppName], [ClientId], [ClientSecret], [RedirectURL])
                      VALUES (@AppName, @ClientId, @ClientSecret, @RedirectURL)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[ClientModels]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[ClientModels] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.ClientModel_Update",
                p => new
                    {
                        Id = p.Int(),
                        AppName = p.String(),
                        ClientId = p.String(),
                        ClientSecret = p.String(),
                        RedirectURL = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[ClientModels]
                      SET [AppName] = @AppName, [ClientId] = @ClientId, [ClientSecret] = @ClientSecret, [RedirectURL] = @RedirectURL
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.ClientModel_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[ClientModels]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.UserModel_Insert",
                p => new
                    {
                        Email = p.String(),
                        UserName = p.String(),
                        Password = p.String(),
                    },
                body:
                    @"INSERT [dbo].[UserModels]([Email], [UserName], [Password])
                      VALUES (@Email, @UserName, @Password)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[UserModels]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[UserModels] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.UserModel_Update",
                p => new
                    {
                        Id = p.Int(),
                        Email = p.String(),
                        UserName = p.String(),
                        Password = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[UserModels]
                      SET [Email] = @Email, [UserName] = @UserName, [Password] = @Password
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.UserModel_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[UserModels]
                      WHERE ([Id] = @Id)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.UserModel_Delete");
            DropStoredProcedure("dbo.UserModel_Update");
            DropStoredProcedure("dbo.UserModel_Insert");
            DropStoredProcedure("dbo.ClientModel_Delete");
            DropStoredProcedure("dbo.ClientModel_Update");
            DropStoredProcedure("dbo.ClientModel_Insert");
            DropTable("dbo.UserModels");
            DropTable("dbo.ClientModels");
        }
    }
}
