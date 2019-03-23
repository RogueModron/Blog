package app.ms.orderColumn.bidirectional.mappedBy;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.Table;

@Entity(name = "SlaveOrderBiMappedBy")
@Table(name = "slave")
public class Slave
{
	@Id
	/*
	@GeneratedValue(
			strategy = GenerationType.SEQUENCE,
			generator = "sequenceSlaveOrderColumn")
	@SequenceGenerator(allocationSize = 1,
			name = "sequenceSlaveOrderColumn",
			sequenceName = "slave_sequence")
	*/
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private int id = 0;
	
	private String description = "";
	
	@ManyToOne
	@JoinColumn(name = "id_master")
	private Master master = null;
	
	
	protected Slave()
	{
		//
	}
	
	public Slave(
			Master master,
			String description)
	{
		this.master = master;
		this.description = description;
	}
	
	
	public int getId()
	{
		return id;
	}


	public String getDescription()
	{
		return description;
	}

	public void setDescription(String description)
	{
		this.description = description;
	}
	
	
	public Master getMaster()
	{
		return master;
	}
	
	public void clearMaster()
	{
		master = null;
	}
}
