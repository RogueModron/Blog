package app.ms.noOrderColumn.unidirectional;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity(name = "SlaveNoOrderUni")
@Table(name = "slave")
public class Slave
{
	@Id
	/*
	@GeneratedValue(
			strategy = GenerationType.SEQUENCE,
			generator = "sequenceSlaveUnidirectional")
	@SequenceGenerator(allocationSize = 1,
			name = "sequenceSlaveUnidirectional",
			sequenceName = "slave_sequence")
	*/
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private int id = 0;
	
	private String description = "";
	
	
	protected Slave()
	{
		//
	}
	
	public Slave(
			String description)
	{
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
}
