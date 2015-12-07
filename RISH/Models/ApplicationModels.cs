using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace RISH.Models
{
    // Puede agregar datos del perfil del usuario agregando más propiedades a la clase ApplicationUser. Para más información, visite http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(200, ErrorMessage = "El número de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 4)]
        [Display(Name = "Nombres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(200, ErrorMessage = "El número de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 4)]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }
    }

    public class Hardware
    {
        [Display(Name = "Hardware ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Marca")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Modelo")]
        public string Model { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Index("HardwareSerialNumberIndex", IsUnique = true)]
        [StringLength(50, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Nro. Serie")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Display(Name = "Tipo de Hardware")]
        public int HardwareTypeId { get; set; }

        [StringLength(50, ErrorMessage = "El número de caracteres no debe ser mayor a {1}.")]
        public string Property01 { get; set; }

        [StringLength(50, ErrorMessage = "El número de caracteres no debe ser mayor a {1}.")]
        public string Property02 { get; set; }

        [StringLength(50, ErrorMessage = "El número de caracteres no debe ser mayor a {1}.")]
        public string Property03 { get; set; }

        [StringLength(50, ErrorMessage = "El número de caracteres no debe ser mayor a {1}.")]
        public string Property04 { get; set; }

        [StringLength(50, ErrorMessage = "El número de caracteres no debe ser mayor a {1}.")]
        public string Property05 { get; set; }

        [StringLength(50, ErrorMessage = "El número de caracteres no debe ser mayor a {1}.")]
        public string Property06 { get; set; }

        [StringLength(50, ErrorMessage = "El número de caracteres no debe ser mayor a {1}.")]
        public string Property07 { get; set; }

        [StringLength(50, ErrorMessage = "El número de caracteres no debe ser mayor a {1}.")]
        public string Property08 { get; set; }

        [StringLength(50, ErrorMessage = "El número de caracteres no debe ser mayor a {1}.")]
        public string Property09 { get; set; }

        [StringLength(50, ErrorMessage = "El número de caracteres no debe ser mayor a {1}.")]
        public string Property10 { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Display(Name = "Proveedor")]
        public int ProviderId { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de compra")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [DataType(DataType.Date)]
        [Display(Name = "Vencimiento de Garantía")]
        public DateTime WarrantyExpirationDate { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Display(Name = "Número de factura")]
        public int InvoiceNumber { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de factura")]
        public DateTime InvoiceDate { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Display(Name = "Estado")]
        public int HardwareStatusId { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Display(Name = "Ubicación")]
        public int LocationId { get; set; }

        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }

        [ForeignKey("HardwareTypeId")]
        public virtual HardwareType Type { get; set; }

        [ForeignKey("HardwareStatusId")]
        public virtual HardwareStatus Status { get; set; }

        public virtual ICollection<InstalledSoftware> InstalledSoftwares { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
    }

    public class HardwareType
    {
        [Display(Name = "Tipo ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Index("HardwareTypeNameIndex", IsUnique = true)]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        public virtual ICollection<Hardware> Hardwares { get; set; }
    }

    public class HardwareStatus
    {
        [Display(Name = "Estado ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Index("HardwareStatusNameIndex", IsUnique = true)]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        public virtual ICollection<Hardware> Hardwares { get; set; }
    }

    public class Software
    {
        [Display(Name = "Software ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Display(Name = "Vendor")]
        public int VendorId { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Display(Name = "Tipo de Licencia")]
        public int LicenseTypeId { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Display(Name = "Arquitectura")]
        public int SoftwareArchitectureId { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Display(Name = "Proveedor")]
        public int ProviderId { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de compra")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Display(Name = "Número de Factura")]
        public int InvoiceNumber { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Factura")]
        public DateTime InvoiceDate { get; set; }

        [ForeignKey("VendorId")]
        public virtual Vendor Vendor { get; set; }

        [ForeignKey("LicenseTypeId")]
        public virtual LicenseType LicenseType { get; set; }

        [ForeignKey("SoftwareArchitectureId")]
        public virtual SoftwareArchitecture SoftwareArchitecture { get; set; }

        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }

        public virtual ICollection<InstalledSoftware> InstalledSoftwares { get; set; }
    }

    public class LicenseType
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Index("LicenseTypeNameIndex", IsUnique = true)]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        public virtual ICollection<Software> Softwares { get; set; }
    }

    public class SoftwareArchitecture
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Index("SoftwareArchitectureNameIndex", IsUnique = true)]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        public virtual ICollection<Software> Softwares { get; set; }
    }

    public class Vendor
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Index("VendorNameIndex", IsUnique = true)]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        public virtual ICollection<Software> Softwares { get; set; }
    }

    public class Provider
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Index("ProviderRUTIndex", IsUnique = true)]
        [StringLength(12, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.", MinimumLength = 10)]
        [Display(Name = "RUT")]
        public string RUT { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [Index("ProviderNameIndex", IsUnique = true)]
        [StringLength(200, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(200, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Razón Social")]
        public string BusinessName { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(250, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(30, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Teléfono")]
        public string PhoneNumber { get; set; }
    }

    public class InstalledSoftware
    {
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Index("HardwareSoftwareIndex", IsUnique = true, Order = 1)]
        public int HardwareId { get; set; }

        [Index("HardwareSoftwareIndex", IsUnique = true, Order = 2)]
        public int SoftwareId { get; set; }

        [ForeignKey("HardwareId")]
        public virtual Hardware Hardware { get; set; }

        [ForeignKey("SoftwareId")]
        public virtual Software Software { get; set; }
    }

    public class Location
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [Index("LocationNameIndex", IsUnique = true)]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "El número de caracteres de {0} no debe ser mayor a {1}.")]
        [Display(Name = "Responsable")]
        public string OwnerName { get; set; }
    }
}