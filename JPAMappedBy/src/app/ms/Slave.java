package app.ms;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity(name = "Slave")
@Table(name = "slave")
public class Slave
{
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private int id = 0;
	
	protected Slave()
	{
		//
	}
	
	public int getId()
	{
		return id;
	}
}
