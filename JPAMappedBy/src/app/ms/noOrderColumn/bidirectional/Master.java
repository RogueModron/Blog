package app.ms.noOrderColumn.bidirectional;

import java.util.ArrayList;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.OneToMany;
import javax.persistence.Table;

@Entity(name = "MasterNoOrderBi")
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
			cascade = CascadeType.ALL,
			orphanRemoval = true)
	@JoinColumn(name = "id_master")
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
