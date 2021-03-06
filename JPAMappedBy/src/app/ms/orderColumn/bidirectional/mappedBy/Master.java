package app.ms.orderColumn.bidirectional.mappedBy;

import java.util.ArrayList;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;
import javax.persistence.OrderColumn;
import javax.persistence.Table;

@Entity(name = "MasterOrderBiMappedBy")
@Table(name = "master")
public class Master
{
	@Id
	/*
	@GeneratedValue(
			strategy = GenerationType.SEQUENCE,
			generator = "sequenceMasterOrderColumn")
	@SequenceGenerator(allocationSize = 1,
			name = "sequenceMasterOrderColumn",
			sequenceName = "master_sequence")
	*/
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private int id = 0;
	
	private String description = "";
	
	@OneToMany(
			mappedBy = "master",
			cascade = CascadeType.ALL,
			orphanRemoval = true)
	@OrderColumn(name = "index", nullable = false)
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
	
	public void removeSlave(String description)
	{
		/* The slave remains on db when using this code and OpenJPA:
		
		slaves.removeIf(slave -> {
			if (slave.getDescription().equals(description))
			{
				slave.clearMaster();
				return true;
			}
			return false;
		});
		
		*/
		
		for (int i = slaves.size() - 1; i >= 0; i--)
		{
			Slave s = slaves.get(i);
			if (s.getDescription().equals(description))
			{
				s.clearMaster();
				slaves.remove(i);
			}
		}
	}
}

