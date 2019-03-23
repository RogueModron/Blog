package app.ms.orderColumn.unidirectional;

import java.util.ArrayList;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.OneToMany;
import javax.persistence.OrderColumn;
import javax.persistence.Table;

@Entity(name = "MasterOrderUni")
@Table(name = "master")
public class Master
{
	@Id
	/*
	@GeneratedValue(
			strategy = GenerationType.SEQUENCE,
			generator = "sequenceMasterUnidirectional")
	@SequenceGenerator(allocationSize = 1,
			name = "sequenceMasterUnidirectional",
			sequenceName = "master_sequence")
	*/
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private int id = 0;
	
	private String description = "";

	/*
		Nullable fk for EclipseLink:
			https://stackoverflow.com/questions/12755380/jpa-persisting-a-unidirectional-one-to-many-relationship-fails-with-eclipselin
	*/
	@OneToMany(
			cascade = CascadeType.ALL,
			orphanRemoval = true)
	@JoinColumn(name = "id_master", nullable = true)
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
		Slave slave = new Slave(description);
		slaves.add(slave);
	}
	
	public void removeSlave(String description)
	{
		for (int i = slaves.size() - 1; i >= 0; i--)
		{
			Slave s = slaves.get(i);
			if (s.getDescription().equals(description))
			{
				slaves.remove(i);
			}
		}
	}
}
