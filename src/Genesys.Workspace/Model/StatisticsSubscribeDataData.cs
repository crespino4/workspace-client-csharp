/* 
 * Workspace API
 *
 * Agent API
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = Genesys.Workspace.Client.SwaggerDateConverter;

namespace Genesys.Workspace.Model
{
    /// <summary>
    /// StatisticsSubscribeDataData
    /// </summary>
    [DataContract]
    public partial class StatisticsSubscribeDataData :  IEquatable<StatisticsSubscribeDataData>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsSubscribeDataData" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected StatisticsSubscribeDataData() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsSubscribeDataData" /> class.
        /// </summary>
        /// <param name="ConnectionId">ConnectionId (required).</param>
        /// <param name="Statistics">Statistics (required).</param>
        public StatisticsSubscribeDataData(string ConnectionId = default(string), List<StatisticValueForRegister> Statistics = default(List<StatisticValueForRegister>))
        {
            // to ensure "ConnectionId" is required (not null)
            if (ConnectionId == null)
            {
                throw new InvalidDataException("ConnectionId is a required property for StatisticsSubscribeDataData and cannot be null");
            }
            else
            {
                this.ConnectionId = ConnectionId;
            }
            // to ensure "Statistics" is required (not null)
            if (Statistics == null)
            {
                throw new InvalidDataException("Statistics is a required property for StatisticsSubscribeDataData and cannot be null");
            }
            else
            {
                this.Statistics = Statistics;
            }
        }
        
        /// <summary>
        /// Gets or Sets ConnectionId
        /// </summary>
        [DataMember(Name="connectionId", EmitDefaultValue=false)]
        public string ConnectionId { get; set; }

        /// <summary>
        /// Gets or Sets Statistics
        /// </summary>
        [DataMember(Name="statistics", EmitDefaultValue=false)]
        public List<StatisticValueForRegister> Statistics { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class StatisticsSubscribeDataData {\n");
            sb.Append("  ConnectionId: ").Append(ConnectionId).Append("\n");
            sb.Append("  Statistics: ").Append(Statistics).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as StatisticsSubscribeDataData);
        }

        /// <summary>
        /// Returns true if StatisticsSubscribeDataData instances are equal
        /// </summary>
        /// <param name="other">Instance of StatisticsSubscribeDataData to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(StatisticsSubscribeDataData other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.ConnectionId == other.ConnectionId ||
                    this.ConnectionId != null &&
                    this.ConnectionId.Equals(other.ConnectionId)
                ) && 
                (
                    this.Statistics == other.Statistics ||
                    this.Statistics != null &&
                    this.Statistics.SequenceEqual(other.Statistics)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                if (this.ConnectionId != null)
                    hash = hash * 59 + this.ConnectionId.GetHashCode();
                if (this.Statistics != null)
                    hash = hash * 59 + this.Statistics.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
