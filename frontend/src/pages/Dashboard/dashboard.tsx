import { useEffect, useState } from "react";
import AlertToast from "../../components/alertToast";
import DashboardNav from "../../components/dashboardNav";
import { BACKEND_URL } from "../../assets/globalVariables"; 

// Define the Alert type based on your API response structure
export interface Alert {
  id: string;
  message: string;
  status: string;
  timestamp: string;
}

function Dashboard() {
  const [alerts, setAlerts] = useState<Alert[]>([]);

  useEffect(() => {
    const fetchAlerts = async () => {
      try {
        const response = await fetch(`https://localhost:7285/api/Auth`);
        if (response.ok) {
          const data: Alert[] = await response.json();
          setAlerts(data);
        } else {
          setAlerts([]);
        }
      } catch (error) {
        setAlerts([]);
      }
    };
    fetchAlerts();
  }, []);

  return (
    <>
      <div className={`alert`}>
        {alerts.map((alert) => (
          <AlertToast key={alert.id} id={alert.id} message={alert.message} status={alert.status} timestamp={alert.timestamp} />
        ))}
        
      </div>
      <DashboardNav />
    </>
  );
}

export default Dashboard;