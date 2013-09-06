<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChildrenControl.ascx.cs" Inherits="Innovaction.Modules.CustomerUpdate.ChildrenControl" %>



<div class="container">
                                <div class="row">
                               
                                   

                                    <div class="column four">
                                      <asp:Label ID="tx_KidDate" runat="server" Text="Fecha" HelpText="It's the name of the thing" />
                              
                                    </div>
                                    <div class="column four">
                                        <asp:Label ID="tx_KidGender" runat="server" Text="Sexo" HelpText="It's the name of the thing" />
                                    </div>

                                    <div class="column four">
                                        <asp:LinkButton ID="Delete" runat="server" OnClick="Delete_Click" Text="Eliminar" />
                                    </div>
                                   <div class="column four">
                                          </div>
                                 

                                </div>
                            </div>
<asp:CheckBox CssClass="texto" ID="chk_IsNew" runat="server" Checked="True" Visible="False" />
<asp:Label CssClass="texto" ID="tx_KidNO" runat="server" Text="Numero" Visible="False" HelpText="It's the name of the thing" />
                                  