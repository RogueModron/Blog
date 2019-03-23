package app.ms;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity(name = "Master")
@Table(name = "master")
public class Master
{
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private int id = 0;
	
	protected Master()
	{
		//
	}
	
	public int getId()
	{
		return id;
	}
}
