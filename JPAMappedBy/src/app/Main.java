package app;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;
import javax.persistence.Query;
import javax.persistence.TypedQuery;
import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Root;

public class Main
{
	public static void main(String[] args)
	{
		/*
			OpenJPA needs enhancement for query execution:
				https://stackoverflow.com/questions/53085855/jpa-open-jpa-onetomany-failedobject

			Set VM arguments, for example:
				-javaagent:C:/Users/XYZ/.m2/repository/org/apache/openjpa/openjpa/3.0.0/openjpa-3.0.0.jar
		*/

		EntityManagerFactory emf = Persistence.createEntityManagerFactory("JPAMappedByHB");
//		EntityManagerFactory emf = Persistence.createEntityManagerFactory("JPAMappedByEL");
//		EntityManagerFactory emf = Persistence.createEntityManagerFactory("JPAMappedByOJ");

		
		{
			EntityManager em = emf.createEntityManager();
			em.getTransaction().begin();
			
			Query q = em.createQuery("delete from Slave");
			q.executeUpdate();
			q = em.createQuery("delete from Master");
			q.executeUpdate();
			
			em.getTransaction().commit();
			em.close();
		}
		System.out.println("");
		
		
		System.out.println("***** Unidirectional *****");
		{
			app.ms.noOrderColumn.unidirectional.Master master = new app.ms.noOrderColumn.unidirectional.Master("Devil");
			master.addSlave("Devil's slave A");
			master.addSlave("Devil's slave B");
			master.addSlave("Devil's slave C");
			
			EntityManager em = emf.createEntityManager();
			em.getTransaction().begin();
			
			em.persist(master);
			
			em.getTransaction().commit();
			em.close();
		}
		System.out.println("");
		
		System.out.println("***** Bidirectional *****");
		{
			app.ms.noOrderColumn.bidirectional.Master master = new app.ms.noOrderColumn.bidirectional.Master("Teufel");
			master.addSlave("Teufel's slave A");
			master.addSlave("Teufel's slave B");
			master.addSlave("Teufel's slave C");
			
			EntityManager em = emf.createEntityManager();
			em.getTransaction().begin();
			
			em.persist(master);
			
			em.getTransaction().commit();
			em.close();
		}
		System.out.println("");
		
		System.out.println("***** Bidirectional mappedBy *****");
		{
			app.ms.noOrderColumn.bidirectional.mappedBy.Master master = new app.ms.noOrderColumn.bidirectional.mappedBy.Master("Diablo");
			master.addSlave("Diablo's slave A");
			master.addSlave("Diablo's slave B");
			master.addSlave("Diablo's slave C");
			
			EntityManager em = emf.createEntityManager();
			em.getTransaction().begin();
			
			em.persist(master);
			
			em.getTransaction().commit();
			em.close();
		}
		System.out.println("");
		
		
		int IDMasterUni = 0;
		int IDMasterBi = 0;
		int IDMasterBiMappedBy = 0;
		
		
		System.out.println("***** Unidirectional orderColumn *****");
		{
			app.ms.orderColumn.unidirectional.Master master = new app.ms.orderColumn.unidirectional.Master("Devil");
			master.addSlave("Devil's slave A");
			master.addSlave("Devil's slave B");
			master.addSlave("Devil's slave C");
			master.addSlave("Devil's slave D");
			master.addSlave("Devil's slave E");
			master.addSlave("Devil's slave F");
			
			EntityManager em = emf.createEntityManager();
			em.getTransaction().begin();
			
			em.persist(master);
			
			em.getTransaction().commit();
			em.close();
			
			IDMasterUni = master.getId();
		}
		System.out.println("");
		
		System.out.println("***** Bidirectional orderColumn *****");
		{
			app.ms.orderColumn.bidirectional.Master master = new app.ms.orderColumn.bidirectional.Master("Teufel");
			master.addSlave("Teufel's slave A");
			master.addSlave("Teufel's slave B");
			master.addSlave("Teufel's slave C");
			master.addSlave("Teufel's slave D");
			master.addSlave("Teufel's slave E");
			master.addSlave("Teufel's slave F");
			
			EntityManager em = emf.createEntityManager();
			em.getTransaction().begin();
			
			em.persist(master);
			
			em.getTransaction().commit();
			em.close();
			
			IDMasterBi = master.getId();
		}
		System.out.println("");
		
		System.out.println("***** Bidirectional orderColumn mappedBy *****");
		{
			app.ms.orderColumn.bidirectional.mappedBy.Master master = new app.ms.orderColumn.bidirectional.mappedBy.Master("Diablo");
			master.addSlave("Diablo's slave A");
			master.addSlave("Diablo's slave B");
			master.addSlave("Diablo's slave C");
			master.addSlave("Diablo's slave D");
			master.addSlave("Diablo's slave E");
			master.addSlave("Diablo's slave F");
			
			EntityManager em = emf.createEntityManager();
			em.getTransaction().begin();
			
			em.persist(master);
			
			em.getTransaction().commit();
			em.close();
			
			IDMasterBiMappedBy = master.getId();
		}
		System.out.println("");
		
		
		System.out.println("***** Unidirectional OrderColumn Delete *****");
		{
			EntityManager em = emf.createEntityManager();
			em.getTransaction().begin();
			
			app.ms.orderColumn.unidirectional.Master master = em.find(app.ms.orderColumn.unidirectional.Master.class, IDMasterUni);
			master.removeSlave("Devil's slave E");
			
			em.getTransaction().commit();
			em.close();
		}
		System.out.println("");
		
		System.out.println("***** Bidirectional OrderColumn Delete *****");
		{
			EntityManager em = emf.createEntityManager();
			em.getTransaction().begin();
			
			app.ms.orderColumn.bidirectional.Master master = em.find(app.ms.orderColumn.bidirectional.Master.class, IDMasterBi);
			master.removeSlave("Teufel's slave E");
			
			em.getTransaction().commit();
			em.close();
		}
		System.out.println("");
		
		System.out.println("***** Bidirectional mappedBy OrderColumn Delete *****");
		{
			EntityManager em = emf.createEntityManager();
			em.getTransaction().begin();
			
			app.ms.orderColumn.bidirectional.mappedBy.Master master = em.find(app.ms.orderColumn.bidirectional.mappedBy.Master.class, IDMasterBiMappedBy);
			master.removeSlave("Diablo's slave E");
			
			em.getTransaction().commit();
			em.close();
		}
		System.out.println("");
		
		
		System.out.println("***** Unidirectional Fetch EAGER With Criteria *****");
		{
			CriteriaBuilder cb = emf.getCriteriaBuilder();
			CriteriaQuery<app.ms.noOrderColumn.unidirectional.Master> criteria = cb.createQuery(app.ms.noOrderColumn.unidirectional.Master.class);
			Root<app.ms.noOrderColumn.unidirectional.Master> root = criteria.from(app.ms.noOrderColumn.unidirectional.Master.class);
			root.fetch("slaves");
			criteria.select(root);
			
			EntityManager em = emf.createEntityManager();
			em.getTransaction().begin();
			
			TypedQuery<app.ms.noOrderColumn.unidirectional.Master> query = em.createQuery(criteria);
			query.getResultList();
			
			em.getTransaction().rollback();
			em.close();
		}
		System.out.println("");
		
		/*
		System.out.println("***** Unidirectional Fetch EAGER With jpql *****");
		{
			EntityManager em = emf.createEntityManager();
			em.getTransaction().begin();
			
			Query query = em.createQuery("select m from MasterNoOrderUni m join fetch m.slaves");
			query.getResultList();
			
			em.getTransaction().rollback();
			em.close();
		}
		System.out.println("");
		*/
		
		
		System.out.println("***** Bidirectional mappedBy Fetch EAGER With Criteria *****");
		{
			CriteriaBuilder cb = emf.getCriteriaBuilder();
			CriteriaQuery<app.ms.noOrderColumn.bidirectional.mappedBy.Master> criteria = cb.createQuery(app.ms.noOrderColumn.bidirectional.mappedBy.Master.class);
			Root<app.ms.noOrderColumn.bidirectional.mappedBy.Master> root = criteria.from(app.ms.noOrderColumn.bidirectional.mappedBy.Master.class);
			root.fetch("slaves");
			criteria.select(root);
			
			EntityManager em = emf.createEntityManager();
			em.getTransaction().begin();
			
			TypedQuery<app.ms.noOrderColumn.bidirectional.mappedBy.Master> query = em.createQuery(criteria);
			query.getResultList();
			
			em.getTransaction().rollback();
			em.close();
		}
		System.out.println("");
		
		/*
		System.out.println("***** Bidirectional mappedBy Fetch EAGER With jpql *****");
		{
			EntityManager em = emf.createEntityManager();
			em.getTransaction().begin();
			
			Query query = em.createQuery("select m from MasterNoOrderBiMappedBy m join fetch m.slaves");
			query.getResultList();
			
			em.getTransaction().rollback();
			em.close();
		}
		System.out.println("");
		*/
		
		emf.close();
	}
}
