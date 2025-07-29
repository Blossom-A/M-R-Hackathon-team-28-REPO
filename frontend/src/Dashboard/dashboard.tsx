import React from "react";
import DashboardNav from "../components/dashboardNav";
import AlertToast from "../components/alertToast";

function dashboard() {
  return (
    <>
      <div className={`alert`}>
        <AlertToast />
      </div>
      <DashboardNav />
      <section>dashboard</section>
    </>
  );
}

export default dashboard;
