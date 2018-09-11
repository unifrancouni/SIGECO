//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIGECO
{
    using System;
    using System.Collections.Generic;
    
    public partial class Expediente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Expediente()
        {
            this.AntecedenteDental = new HashSet<AntecedenteDental>();
            this.AntecedenteFamiliar = new HashSet<AntecedenteFamiliar>();
            this.Cita = new HashSet<Cita>();
            this.ExamenComplementario = new HashSet<ExamenComplementario>();
            this.Odontograma = new HashSet<Odontograma>();
            this.Periodontograma = new HashSet<Periodontograma>();
            this.Placagrama = new HashSet<Placagrama>();
            this.TratamientoRealizado = new HashSet<TratamientoRealizado>();
        }
    
        public int nExpedienteID { get; set; }
        public int nPacienteID { get; set; }
        public int nCodigoExpediente { get; set; }
        public System.DateTime dFechaExpediente { get; set; }
        public string sACPPESA { get; set; }
        public int nGrupoSanguineoID { get; set; }
        public byte nAAAMA { get; set; }
        public string sAAAMA { get; set; }
        public int nEstadoID { get; set; }
        public System.DateTime dFechaCreacion { get; set; }
        public string sUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> dFechaModificacion { get; set; }
        public string sUsuarioModificacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AntecedenteDental> AntecedenteDental { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AntecedenteFamiliar> AntecedenteFamiliar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cita> Cita { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamenComplementario> ExamenComplementario { get; set; }
        public virtual Paciente Paciente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Odontograma> Odontograma { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Periodontograma> Periodontograma { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Placagrama> Placagrama { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TratamientoRealizado> TratamientoRealizado { get; set; }
    }
}