package app.ms.noOrderColumn.bidirectional.mappedBy;

import java.util.ArrayList;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;
import javax.persistence.Table;

@Entity(name = "MasterNoOrderBiMappedBy")
@Table(name = "master")
public class Master
{
	@Id
	/*
	@GeneratedValue(
			strategy = GenerationType.SEQUENCE,
			generator = "sequenceMasterBidirectional")
	@SequenceGenerator(allocationSize = 1,
			name = "sequenceMasterBidirectional",
			sequenceName = "master_sequence")
	*/
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private int id = 0;
	
	private String description = "";
	
	@OneToMany(
			mappedBy = "master",
			cascade = CascadeType.ALL,
			orphanRemoval = true)
	private List<Slave> slaves = null;
	
	
	protected Master()
	{
		//
	}
	
	public Master(String description)
	{
		this.description = description;
		this.slaves = new ArrayList<>();
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
	
	
	public void addSlave(String description)
	{
		Slave slave = new Slave(this, description);
		slaves.add(slave);
	}
}
