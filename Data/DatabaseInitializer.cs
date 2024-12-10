using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using groveale.Data;
using groveale.Models;
using System;
using System.Globalization;

namespace groveale.Data
{
    public static class DatabaseInitializer
    {
        public static void EnsureDatabasesCreated(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var ticketDbContext = services.GetRequiredService<TicketDb>();
                ticketDbContext.Database.EnsureCreated();
                SeedTickets(ticketDbContext);

                var orderDbContext = services.GetRequiredService<OrderDb>();
                orderDbContext.Database.EnsureCreated();
                SeedOrders(orderDbContext);

                var opportunityDbContext = services.GetRequiredService<OpportunityDb>();
                opportunityDbContext.Database.EnsureCreated();
                SeedOpportunities2(opportunityDbContext);

                var customerDbContext = services.GetRequiredService<CustomerDb>();
                customerDbContext.Database.EnsureCreated();
                SeedCustomers(customerDbContext);

                var orderHeaderDbContext = services.GetRequiredService<OrderHeaderDb>();
                orderHeaderDbContext.Database.EnsureCreated();
                SeedOrderHeaders(orderHeaderDbContext);

                var invoiceDbContext = services.GetRequiredService<InvoiceDb>();
                invoiceDbContext.Database.EnsureCreated();
                SeedInvoices(invoiceDbContext);
            }
        }

        

        private static void SeedTickets(TicketDb context)
        {
            string csvData = @"I can't access my account and need a password reset ASAP! This is affecting my work.	High	allen.trevino@Bosch.com	New	Fatoumata.Diallo@CCSpark.io	28/04/2024	30/04/2024	Bosch	2	2
                    VPN is down, and I can't work remotely. Fix this now! This is unacceptable.	Critical	brenda.sanchez@BMWGroup.com	InProgress	Mei.Ling@CCSpark.io	09/08/2024	21/08/2024	BMW Group	3	12
                    The office printer is not working again. Please fix it as soon as possible.	Medium	patricia.lopez@BMWGroup.com	Resolved	Hunter.Stephens@CCSpark.io	01/01/2024	02/01/2024	BMW Group	3	1
                    I need new software installed on my computer. It's for a project due next week.	Low	kimberly.medina@Bosch.com	New	Sharon.Compton@CCSpark.io	03/10/2024	06/10/2024	Bosch	2	3
                    My email isn't syncing on my phone. This is urgent as I need to stay connected.	High	dorothy.bennett@Telefonica.com	New	Sarah.Davis@CCSpark.io	10/04/2024	14/04/2024	Telefonica	7	4
                    The network is down, and we can't do anything. Fix it now! This is a major disruption.	Critical	james.hansen@Bosch.com	New	Daniel.Anderson@CCSpark.io	16/10/2024	22/10/2024	Bosch	2	6
                    I need a new laptop for my work. The current one is too slow.	Medium	brandon.myers@BMWGroup.com	Resolved	Matthew.Jackson@CCSpark.io	26/07/2024	03/08/2024	BMW Group	3	8
                    The application keeps crashing. I need this fixed immediately as it's hindering my work.	High	evan.mercado@AudiAG.com	In Progress	Joshua.Harris@CCSpark.io	28/09/2024	30/09/2024	Audi AG	5	2
                    Please back up my data. I have important files that need to be secured.	Low	sarah.solis@BMWGroup.com	In Progress	Brandon.Hall@CCSpark.io	02/08/2024	24/08/2024	BMW Group	3	22
                    We've had a security breach. This needs immediate attention to prevent data loss.	Critical	charles.bauer@Bosch.com	Resolved	Christopher.Wright@CCSpark.io	24/01/2024	30/01/2024	Bosch	2	6
                    My account is locked, and I can't log in. Please unlock it as soon as possible.	High	russell.marsh@Bosch.com	New	Justin.Baker@CCSpark.io	15/12/2023	12/03/2024	Bosch	2	88
                    I need the latest software update installed. The current version is outdated.	Medium	corey.wilson@AudiAG.com	New	Kevin.Perez@CCSpark.io	09/10/2024	12/10/2024	Audi AG	5	3
                    Emails are not being delivered. This is urgent as I am missing important communications.	High	melissa.hall@AudiAG.com	In Progress	Fatoumata.Diallo@CCSpark.io	22/07/2024	26/07/2024	Audi AG	5	4
                    The internet is very slow. Please check and resolve this issue.	Medium	kathryn.nichols@BMWGroup.com	New	Mei.Ling@CCSpark.io	08/07/2024	11/07/2024	BMW Group	3	3
                    I need access to the shared drive for team collaboration.	Low	elizabeth.grant@AudiAG.com	New	Hunter.Stephens@CCSpark.io	15/06/2024	24/06/2024	Audi AG	5	9
                    The video conferencing tool isn't working. This is urgent as I have meetings scheduled.	High	amy.johnson@AudiAG.com	In Progress	Sharon.Compton@CCSpark.io	18/12/2023	12/02/2024	Audi AG	5	56
                    I need a new email account set up for a new employee.	Medium	steven.martin@AOKPlus.com	Resolved	Sarah.Davis@CCSpark.io	22/08/2024	26/08/2024	AOK Plus	6	4
                    Please renew my software license. It is about to expire.	Low	jennifer.martinez@AOKPlus.com	Resolved	Daniel.Anderson@CCSpark.io	26/10/2023	06/11/2023	AOK Plus	6	11
                    My computer is malfunctioning. I need this fixed immediately to continue my work.	High	christopher.lee@AOKPlus.com	Resolved	Matthew.Jackson@CCSpark.io	05/06/2024	14/06/2024	AOK Plus	6	9
                    I need remote desktop access set up to work from home.	Medium	margaret.moon@AudiAG.com	Resolved	Joshua.Harris@CCSpark.io	11/06/2023	15/06/2023	Audi AG	5	4
                    The file sharing service isn't working. This is urgent as I need to share files with my team.	High	vanessa.smith@AudiAG.com	New	Brandon.Hall@CCSpark.io	12/09/2023	15/09/2023	Audi AG	5	3
                    I need more storage space for my projects.	Low	chloe.butler@BMWGroup.com	New	Christopher.Wright@CCSpark.io	01/06/2024	13/06/2024	BMW Group	3	12
                    Please set up the network printer. We need it for printing documents.	Medium	ryan.lawrence@Bosch.com	In Progress	Justin.Baker@CCSpark.io	24/01/2024	28/01/2024	Bosch	2	4
                    I need training on the new software. Please schedule a session.	Low	michael.liu@Bosch.com	In Progress	Kevin.Perez@CCSpark.io	22/12/2023	17/01/2024	Bosch	2	26
                    I can't access the cloud service. This is urgent as I need to retrieve files.	High	javier.wright@Telefonica.com	Resolved	Fatoumata.Diallo@CCSpark.io	23/11/2023	10/01/2024	Telefonica	7	48
                    Please create a new user account for a new team member.	Medium	jessica.lyons@Santander.com	New	Mei.Ling@CCSpark.io	04/09/2024	03/10/2024	Santander	1	29
                    My email account has been hacked. This needs immediate attention to secure my data.	Critical	karina.sanchez@Santander.com	Resolved	Hunter.Stephens@CCSpark.io	16/08/2023	08/09/2023	Santander	1	23
                    I need help with my mobile device. It isn't syncing with my work email.	High	elaine.sharp@BMWGroup.com	Resolved	Sharon.Compton@CCSpark.io	21/06/2024	06/08/2024	BMW Group	3	46
                    I can't access the database. Please fix this as I need to retrieve information.	Medium	earl.mitchell@AOKPlus.com	Resolved	Sarah.Davis@CCSpark.io	05/06/2023	07/06/2023	AOK Plus	6	2
                    Please set up VPN access for me. I need it for remote work.	Low	sharon.cain@BMWGroup.com	New	Daniel.Anderson@CCSpark.io	15/03/2023	20/03/2023	BMW Group	3	5
                    The software isn't compatible with my system. This is urgent as I need it for my work.	High	jeffrey.torres@AOKPlus.com	Resolved	Matthew.Jackson@CCSpark.io	24/07/2024	24/07/2024	AOK Plus	6	0
                    Please configure the network settings. We are experiencing connectivity issues.	Medium	ian.singh@Bosch.com	Resolved	Joshua.Harris@CCSpark.io	19/03/2024	25/03/2024	Bosch	2	6
                    Remote access isn't working. This is urgent as I need to access my work files.	High	jessica.lyons@Santander.com	New	Brandon.Hall@CCSpark.io	23/11/2024	24/11/2024	Santander	1	1
                    I need data recovery services. I accidentally deleted important files.	Low	karina.sanchez@Santander.com	Resolved	Christopher.Wright@CCSpark.io	16/09/2024	20/09/2024	Santander	1	4
                    I can't open my email attachements again. This is the third time this month that the Outlook app is crashing and preventing me from work,. This is super annoying!!! 	Medium	elaine.sharp@BMWGroup.com	New	Justin.Baker@CCSpark.io	15/04/2024	18/04/2024	BMW Group	3	3
                    We need urgent support for our ongoing consulting project. The deadline is approaching, and we are facing several issues.	High	earl.mitchell@AOKPlus.com	In Progress	Kevin.Perez@CCSpark.io	22/10/2024	29/10/2024	AOK Plus	6	7
                    The laptops we ordered have not been delivered yet. This delay is unacceptable and affecting our operations.	Critical	sharon.cain@BMWGroup.com	In Progress	Fatoumata.Diallo@CCSpark.io	27/05/2024	05/06/2024	BMW Group	3	9
                    We need a review of the milestones for our consulting project. Please schedule a meeting.	Medium	jeffrey.torres@AOKPlus.com	New	Mei.Ling@CCSpark.io	09/11/2024	17/11/2024	AOK Plus	6	8
                    The laptops were delivered without the necessary accessories. Please send them as soon as possible.	Low	ian.singh@Bosch.com	In Progress	Hunter.Stephens@CCSpark.io	13/11/2024	23/11/2024	Bosch	2	10
                    We need clarification on the scope of the consulting project. There are some ambiguities that need to be addressed.	High	jessica.lyons@Santander.com	In Progress	Sharon.Compton@CCSpark.io	26/08/2023	06/09/2023	Santander	1	11
                    The laptops delivered are not the models we ordered. This needs to be corrected immediately.	Critical	karina.sanchez@Santander.com	Resolved	Sarah.Davis@CCSpark.io	06/07/2024	18/07/2024	Santander	1	12
                    We need additional resources allocated to our consulting project. The current team is insufficient.	Medium	elaine.sharp@BMWGroup.com	In Progress	Daniel.Anderson@CCSpark.io	18/07/2024	31/07/2024	BMW Group	3	13
                    The materials for our consulting project have not been delivered yet. This delay is causing significant issues.	High	earl.mitchell@AOKPlus.com	In Progress	Matthew.Jackson@CCSpark.io	30/08/2024	13/09/2024	AOK Plus	6	14
                    We need an extension on the timeline for our consulting project. The current deadline is not feasible.	Low	sharon.cain@BMWGroup.com	New	Joshua.Harris@CCSpark.io	16/09/2023	01/10/2023	BMW Group	3	15
                    The laptops delivered are damaged. This is unacceptable and needs to be resolved immediately.	Critical	jeffrey.torres@AOKPlus.com	Resolved	Brandon.Hall@CCSpark.io	09/06/2024	25/06/2024	AOK Plus	6	16
                    We need to adjust the budget for our consulting project. There have been some unexpected expenses.	High	ian.singh@Bosch.com	In Progress	Christopher.Wright@CCSpark.io	05/05/2024	22/05/2024	Bosch	2	17
                    The documentation for the delivered items is missing. Please provide it as soon as possible.	Medium	jessica.lyons@Santander.com	Resolved	Justin.Baker@CCSpark.io	14/11/2023	02/12/2023	Santander	1	18
                    We need a status update on our consulting project. There has been no communication for a while.	High	karina.sanchez@Santander.com	Resolved	Kevin.Perez@CCSpark.io	26/12/2024	30/12/2024	Santander	1	4
                    The quantity of laptops delivered is incorrect. We ordered more than what was delivered.	Medium	elaine.sharp@BMWGroup.com	Resolved	Fatoumata.Diallo@CCSpark.io	24/08/2024	30/08/2024	BMW Group	3	6
                    We need a review of the deliverables for our consulting project. Please schedule a meeting.	Low	earl.mitchell@AOKPlus.com	In Progress	Mei.Ling@CCSpark.io	23/08/2024	31/08/2024	AOK Plus	6	8
                    The accessories we ordered have not been delivered yet. This delay is affecting our operations.	High	jessica.lyons@Santander.com	In Progress	Fatoumata.Diallo@CCSpark.io	30/12/2023	01/01/2024	Santander	1	2
                    We need a risk assessment for our consulting project. There are some potential issues that need to be addressed.	Medium	karina.sanchez@Santander.com	Resolved	Mei.Ling@CCSpark.io	22/02/2024	15/03/2024	Santander	1	22
                    Some items are missing from the delivered order. Please send the missing items as soon as possible.	Low	elaine.sharp@BMWGroup.com	In Progress	Hunter.Stephens@CCSpark.io	26/03/2024	01/04/2024	BMW Group	3	6
                    We need to schedule a team meeting for our consulting project. There are some important issues to discuss.	High	earl.mitchell@AOKPlus.com	Resolved	Sharon.Compton@CCSpark.io	02/05/2024	05/05/2024	AOK Plus	6	3
                    The specifications of the delivered items are incorrect. This needs to be corrected immediately.	Critical	sharon.cain@BMWGroup.com	Resolved	Sarah.Davis@CCSpark.io	07/06/2024	10/06/2024	BMW Group	3	3
                    There is a delay in the deliverables for our consulting project. This needs to be addressed urgently.	High	jeffrey.torres@AOKPlus.com	Resolved	Daniel.Anderson@CCSpark.io	11/07/2024	15/07/2024	AOK Plus	6	4
                    The replacement items we requested have not been delivered yet. Please expedite the delivery.	Medium	ian.singh@Bosch.com	New	Matthew.Jackson@CCSpark.io	18/08/2024	20/08/2024	Bosch	2	2
                    We need to request a change in the scope of our consulting project. The current scope is not feasible.	Low	jessica.lyons@Santander.com	Resolved	Joshua.Harris@CCSpark.io	24/08/2024	25/09/2024	Santander	1	32
                    The warranty information for the delivered items is missing. Please provide it as soon as possible.	High	karina.sanchez@Santander.com	Resolved	Brandon.Hall@CCSpark.io	29/10/2024	30/10/2024	Santander	1	1
                    We are facing a shortage of resources for our consulting project. Additional resources are needed.	Medium	elaine.sharp@BMWGroup.com	New	Christopher.Wright@CCSpark.io	31/10/2024	04/12/2024	BMW Group	3	34
                    The billing for the delivered items is incorrect. Please correct the billing information.	Low	earl.mitchell@AOKPlus.com	Resolved	Justin.Baker@CCSpark.io	18/12/2023	09/01/2024	AOK Plus	6	22
                    There is a quality issue with the deliverables for our consulting project. This needs to be addressed immediately.	High	sharon.cain@BMWGroup.com	New	Kevin.Perez@CCSpark.io	09/01/2024	14/02/2024	BMW Group	3	36
                    The software we ordered has not been delivered yet. This delay is affecting our project timeline.	Medium	jeffrey.torres@AOKPlus.com	In Progress	Fatoumata.Diallo@CCSpark.io	11/02/2024	19/03/2024	AOK Plus	6	37
                    Our consulting project is facing a budget overrun. We need to discuss budget adjustments.	High	ian.singh@Bosch.com	In Progress	Mei.Ling@CCSpark.io	19/03/2024	26/04/2024	Bosch	2	38
                    The user manuals for the delivered items are missing. Please provide them as soon as possible.	Low	jessica.lyons@Santander.com	New	Hunter.Stephens@CCSpark.io	21/04/2024	30/05/2024	Santander	1	39
                    We need urgent support for our ongoing consulting project. The deadline is approaching, and we are facing several issues.	High	karina.sanchez@Santander.com	In Progress	Sharon.Compton@CCSpark.io	25/05/2024	04/07/2024	Santander	1	40
                    The laptops we ordered have not been delivered yet. This delay is unacceptable and affecting our operations.	Critical	elaine.sharp@BMWGroup.com	In Progress	Sarah.Davis@CCSpark.io	05/08/2024	09/08/2024	BMW Group	3	4
                    We need a review of the milestones for our consulting project. Please schedule a meeting.	Medium	earl.mitchell@AOKPlus.com	Resolved	Daniel.Anderson@CCSpark.io	08/09/2024	14/09/2024	AOK Plus	6	6
                    The laptops were delivered without the necessary accessories. Please send them as soon as possible.	Low	sharon.cain@BMWGroup.com	In Progress	Matthew.Jackson@CCSpark.io	11/10/2024	19/10/2024	BMW Group	3	8
                    We need clarification on the scope of the consulting project. There are some ambiguities that need to be addressed.	High	jeffrey.torres@AOKPlus.com	In Progress	Joshua.Harris@CCSpark.io	22/11/2024	24/11/2024	AOK Plus	6	2
                    The laptops delivered are not the models we ordered. This needs to be corrected immediately.	Critical	ian.singh@Bosch.com	New	Brandon.Hall@CCSpark.io	07/12/2024	09/12/2024	Bosch	2	2
                    We need additional resources allocated to our consulting project. The current team is insufficient.	Medium	jessica.lyons@Santander.com	Resolved	Christopher.Wright@CCSpark.io	29/12/2023	04/01/2024	Santander	1	6
                    The materials for our consulting project have not been delivered yet. This delay is causing significant issues.	High	karina.sanchez@Santander.com	In Progress	Justin.Baker@CCSpark.io	13/11/2023	09/02/2024	Santander	1	88
                    We need an extension on the timeline for our consulting project. The current deadline is not feasible.	Low	elaine.sharp@BMWGroup.com	Resolved	Kevin.Perez@CCSpark.io	11/03/2024	14/03/2024	BMW Group	3	3
                    The laptops delivered are damaged. This is unacceptable and needs to be resolved immediately.	Critical	earl.mitchell@AOKPlus.com	Resolved	Fatoumata.Diallo@CCSpark.io	17/04/2024	21/04/2024	AOK Plus	6	4
                    We need to adjust the budget for our consulting project. There have been some unexpected expenses.	High	sharon.cain@BMWGroup.com	Resolved	Mei.Ling@CCSpark.io	04/05/2024	26/05/2024	BMW Group	3	22
                    The documentation for the delivered items is missing. Please provide it as soon as possible.	Medium	jeffrey.torres@AOKPlus.com	In Progress	Hunter.Stephens@CCSpark.io	29/05/2024	01/07/2024	AOK Plus	6	33
                    We need a status update on our consulting project. There has been no communication for a while.	High	ian.singh@Bosch.com	In Progress	Sharon.Compton@CCSpark.io	13/06/2024	06/08/2024	Bosch	2	54
                    The quantity of laptops delivered is incorrect. We ordered more than what was delivered.	Medium	jessica.lyons@Santander.com	Resolved	Sarah.Davis@CCSpark.io	06/09/2024	11/09/2024	Santander	1	5
                    We need a review of the deliverables for our consulting project. Please schedule a meeting.	Low	karina.sanchez@Santander.com	In Progress	Daniel.Anderson@CCSpark.io	04/10/2024	16/10/2024	Santander	1	12
                    The accessories we ordered have not been delivered yet. This delay is affecting our operations.	High	elaine.sharp@BMWGroup.com	In Progress	Matthew.Jackson@CCSpark.io	18/11/2024	21/11/2024	BMW Group	3	3
                    We need a risk assessment for our consulting project. There are some potential issues that need to be addressed.	Medium	earl.mitchell@AOKPlus.com	Resolved	Joshua.Harris@CCSpark.io	20/12/2024	26/12/2024	AOK Plus	6	6
                    Some items are missing from the delivered order. Please send the missing items as soon as possible.	Low	sharon.cain@BMWGroup.com	Resolved	Brandon.Hall@CCSpark.io	03/11/2023	01/01/2024	BMW Group	3	59
                    We need to schedule a team meeting for our consulting project. There are some important issues to discuss.	High	jeffrey.torres@AOKPlus.com	Resolved	Christopher.Wright@CCSpark.io	08/12/2023	06/02/2024	AOK Plus	6	60
                    The specifications of the delivered items are incorrect. This needs to be corrected immediately.	Critical	ian.singh@Bosch.com	Resolved	Justin.Baker@CCSpark.io	10/01/2024	11/03/2024	Bosch	2	61
                    There is a delay in the deliverables for our consulting project. This needs to be addressed urgently.	High	jessica.lyons@Santander.com	New	Kevin.Perez@CCSpark.io	14/04/2024	18/04/2024	Santander	1	4
                    The replacement items we requested have not been delivered yet. Please expedite the delivery.	Medium	karina.sanchez@Santander.com	New	Fatoumata.Diallo@CCSpark.io	17/05/2024	23/05/2024	Santander	1	6
                    We need to request a change in the scope of our consulting project. The current scope is not feasible.	Low	elaine.sharp@BMWGroup.com	In Progress	Mei.Ling@CCSpark.io	20/06/2024	28/06/2024	BMW Group	3	8
                    The warranty information for the delivered items is missing. Please provide it as soon as possible.	High	earl.mitchell@AOKPlus.com	In Progress	Hunter.Stephens@CCSpark.io	01/08/2024	03/08/2024	AOK Plus	6	2
                    We are facing a shortage of resources for our consulting project. Additional resources are needed.	Medium	sharon.cain@BMWGroup.com	Resolved	Sharon.Compton@CCSpark.io	17/08/2024	08/09/2024	BMW Group	3	22
                    The billing for the delivered items is incorrect. Please correct the billing information.	Low	jeffrey.torres@AOKPlus.com	New	Sarah.Davis@CCSpark.io	07/10/2024	13/10/2024	AOK Plus	6	6
                    There is a quality issue with the deliverables for our consulting project. This needs to be addressed immediately.	High	ian.singh@Bosch.com	In Progress	Daniel.Anderson@CCSpark.io	20/12/2024	26/12/2024	Bosch	2	6
                    The software we ordered has not been delivered yet. This delay is affecting our project timeline.	Medium	jessica.lyons@Santander.com	Resolved	Matthew.Jackson@CCSpark.io	03/11/2023	01/01/2024	Santander	1	59
                    Our consulting project is facing a budget overrun. We need to discuss budget adjustments.	High	karina.sanchez@Santander.com	Resolved	Joshua.Harris@CCSpark.io	20/12/2024	26/12/2024	Santander	1	6
                    The user manuals for the delivered items are missing. Please provide them as soon as possible.	Low	elaine.sharp@BMWGroup.com	Resolved	Brandon.Hall@CCSpark.io	03/11/2023	01/01/2024	BMW Group	3	59
                    We need urgent support for our ongoing consulting project. The deadline is approaching, and we are facing several issues.	High	earl.mitchell@AOKPlus.com	In Progress	Christopher.Wright@CCSpark.io	08/12/2023	06/02/2024	AOK Plus	6	60
                    The laptops we ordered have not been delivered yet. This delay is unacceptable and affecting our operations.	Critical	sharon.cain@BMWGroup.com	Resolved	Justin.Baker@CCSpark.io	10/01/2024	11/03/2024	BMW Group	3	61
                    We need a review of the milestones for our consulting project. Please schedule a meeting.	Medium	jeffrey.torres@AOKPlus.com	Resolved	Kevin.Perez@CCSpark.io	14/04/2024	18/04/2024	AOK Plus	6	4
                    The laptops were delivered without the necessary accessories. Please send them as soon as possible.	Low	ian.singh@Bosch.com	Resolved	Fatoumata.Diallo@CCSpark.io	17/05/2024	23/05/2024	Bosch	2	6
                    We need clarification on the scope of the consulting project. There are some ambiguities that need to be addressed.	High	jessica.lyons@Santander.com	In Progress	Mei.Ling@CCSpark.io	20/06/2024	28/06/2024	Santander	1	8";

            string[] lines = csvData.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<Ticket> tickets = new List<Ticket>();

            foreach (var line in lines)
            {
                string[] values = line.Split('\t');

                if (values.Length < 10)
                {
                    // Handle the error or log it
                    Console.WriteLine("Error: Line does not contain enough values.");
                    continue;
                }

                try
                {
                    Ticket ticket = new Ticket
                    {
                        Short_Description = string.Empty,
                        Long_Description = values[0].Trim(),
                        Priority = values[1],
                        CallerID = values[2],
                        State = values[3],
                        AssignedTo = values[4],
                        Opened_at = DateTime.ParseExact(values[5], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Closed_at = DateTime.ParseExact(values[6], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        CompanyName = values[7],
                        CompanyID = int.Parse(values[8]),
                        DaysOpen = int.Parse(values[9]),
                    };
                    tickets.Add(ticket);
                }
                catch (Exception ex)
                {
                    // Handle the error or log it
                    Console.WriteLine($"Error processing line: {ex.Message}");
                }
            }

            if (!context.Tickets.Any())
            {
                context.Tickets.AddRange(tickets);
                context.SaveChanges();
            }
        }

        private static void SeedOrders(OrderDb context)
        {
            string csvData = @"1	Santander	Santander BR	LC20	SDN0042	42	1500	Berlin	Microsoft Surface Pro 9	Customer is looking to purchase premium laptops for their German Execs and lead consultants	BR	Rio de Janeiro	Tech Sourcing
                2	Bosch	Bosch FR	LC09	SDN0045	3478	17321	Hatfield	Mobile Phone	Customer is looking to purchase Mobile Phone to enhance communication.	BR	Rio de Janeiro	Tech Sourcing
                2	Bosch	Bosch FR	LC09	SDN0046	5111	4099	Berlin	Managed Services	Customer is looking to purchase Managed Services to improve IT operations.	DE	Berlin	Managed Service
                1	Santander	Santander DE	LC04	SDN0016	657	4039	Tokyo	XR Headset	Customer is looking to purchase XR Headset to innovate training programs.	UK	Birmingham	Consulting
                5	Audi AG	Audi AG UK	LC30	SDN0017	4213	1420	Livermore	Consulting Package	Customer is looking to purchase Consulting Package for strategic planning.	US	Los Angeles	Consulting
                3	BMW Group	BMW Group IE	LC16	SDN0018	623	19441	Livermore	Mobile Phone	Customer is looking to purchase Mobile Phone to enhance communication.	FR	Lyon	Tech Sourcing
                3	BMW Group	BMW Group JP	LC29	SDN0019	374	14935	Hatfield	Consulting Package	Customer is looking to purchase Consulting Package for strategic planning.	BR	Sao Paulo	Consulting
                2	Bosch	Bosch FR	LC09	SDN0020	1266	14673	Roissy CDG	Managed Services	Customer is looking to purchase Managed Services to improve IT operations.	UK	Manchester	Managed Service
                2	Bosch	Bosch FR	LC09	SDN0021	3458	15279	Moordrecht	Microsoft Laptop	Customer is looking to purchase Microsoft Laptop for business productivity.	IT	Rome	Tech Sourcing
                1	Santander	Santander DE	LC04	SDN0022	1458	15686	Hatfield	Mobile Phone	Customer is looking to purchase Mobile Phone to enhance communication.	SA	Riyadh	Tech Sourcing
                1	Santander	Santander DE	LC04	SDN0023	2299	15156	Cape Town	Apple Laptop	Customer is looking to purchase Apple Laptop for creative projects.	DE	Munich	Tech Sourcing
                2	Bosch	Bosch DE	LC28	SDN0024	4386	3279	Berlin	Dell Laptop	Customer is looking to purchase Dell Laptop for reliable performance.	IE	Dublin	Tech Sourcing
                6	AOK Plus	AOK Plus FR	LC14	SDN0025	534	539	Hatfield	XR Headset	Customer is looking to purchase XR Headset to innovate training programs.	FR	Marseille	Tech Sourcing
                2	Bosch	Bosch DE	LC28	SDN0026	5134	18914	Moordrecht	Managed Services	Customer is looking to purchase Managed Services to improve IT operations.	UK	London	Managed Service
                2	Bosch	Bosch DE	LC28	SDN0027	2601	2802	Tokyo	Consulting Package	Customer is looking to purchase Consulting Package for strategic planning.	BR	Brasilia	Consulting
                2	Bosch	Bosch DE	LC28	SDN0028	207	12581	Livermore	XR Headset	Customer is looking to purchase XR Headset to innovate training programs.	UK	Birmingham	Tech Sourcing
                1	Santander	Santander BR	LC20	SDN0029	2703	16758	Roissy CDG	Dell Laptop	Customer is looking to purchase Dell Laptop for reliable performance.	IT	Milan	Tech Sourcing
                5	Audi AG	Audi AG UK	LC30	SDN0030	3781	19002	Doha	Apple Laptop	Customer is looking to purchase Apple Laptop for creative projects.	UK	Manchester	Tech Sourcing
                1	Santander	Santander IE	LC06	SDN0031	1216	17430	Hatfield	Apple Laptop	Customer is looking to purchase Apple Laptop for creative projects.	SA	Jeddah	Tech Sourcing
                5	Audi AG	Audi AG UK	LC30	SDN0032	5440	1867	Livermore	Microsoft Laptop	Customer is looking to purchase Microsoft Laptop for business productivity.	SA	Dammam	Tech Sourcing
                6	AOK Plus	AOK Plus US	LC31	SDN0033	2256	5768	Hatfield	Consulting Package	Customer is looking to purchase Consulting Package for strategic planning.	FR	Paris	Consulting
                1	Santander	Santander BR	LC20	SDN0034	2445	3778	Tokyo	Mobile Phone	Customer is looking to purchase Mobile Phone to enhance communication.	UK	London	Tech Sourcing
                1	Santander	Santander DE	LC04	SDN0016	5516	7511	Berlin	Apple Laptop	Customer is looking to purchase Apple Laptop for creative projects.	US	New York	Tech Sourcing
                5	Audi AG	Audi AG UK	LC30	SDN0017	3884	13193	Moordrecht	Mobile Phone	Customer is looking to purchase Mobile Phone to enhance communication.	FR	Lyon	Tech Sourcing
                3	BMW Group	BMW Group IE	LC16	SDN0018	3003	13840	Berlin	Mobile Phone	Customer is looking to purchase Mobile Phone to enhance communication.	DE	Frankfurt	Tech Sourcing
                3	BMW Group	BMW Group JP	LC29	SDN0019	5964	13388	Tokyo	Dell Laptop	Customer is looking to purchase Dell Laptop for reliable performance.	UK	Birmingham	Tech Sourcing
                2	Bosch	Bosch FR	LC09	SDN0020	762	12689	Cape Town	XR Headset	Customer is looking to purchase XR Headset to innovate training programs.	US	Chicago	Tech Sourcing
                2	Bosch	Bosch FR	LC09	SDN0021	1374	4191	Roissy CDG	Consulting Package	Customer is looking to purchase Consulting Package for strategic planning.	IE	Cork	Consulting
                1	Santander	Santander DE	LC04	SDN0022	2540	13213	Cape Town	Microsoft Laptop	Customer is looking to purchase Microsoft Laptop for business productivity.	JP	Tokyo	Tech Sourcing
                1	Santander	Santander DE	LC04	SDN0023	4746	9159	Roissy CDG	Managed Services	Customer is looking to purchase Managed Services to improve IT operations.	IE	Limerick	Managed Service
                2	Bosch	Bosch DE	LC28	SDN0024	1323	11690	Roissy CDG	Microsoft Laptop	Customer is looking to purchase Microsoft Laptop for business productivity.	JP	Osaka	Tech Sourcing
                6	AOK Plus	AOK Plus FR	LC14	SDN0025	3636	5842	Tokyo	Mobile Phone	Customer is looking to purchase Mobile Phone to enhance communication.	DE	Berlin	Tech Sourcing
                2	Bosch	Bosch DE	LC28	SDN0026	4556	2163	Livermore	Mobile Phone	Customer is looking to purchase Mobile Phone to enhance communication.	DE	Munich	Tech Sourcing
                2	Bosch	Bosch DE	LC28	SDN0027	908	1611	Doha	Microsoft Laptop	Customer is looking to purchase Microsoft Laptop for business productivity.	UK	Manchester	Tech Sourcing
                2	Bosch	Bosch DE	LC28	SDN0028	1684	11435	Tokyo	Dell Laptop	Customer is looking to purchase Dell Laptop for reliable performance.	FR	Paris	Tech Sourcing
                1	Santander	Santander BR	LC20	SDN0029	3646	13885	Roissy CDG	Managed Services	Customer is looking to purchase Managed Services to improve IT operations.	UK	London	Managed Service
                5	Audi AG	Audi AG UK	LC30	SDN0030	140	15649	Cape Town	Dell Laptop	Customer is looking to purchase Dell Laptop for reliable performance.	IT	Naples	Tech Sourcing
                1	Santander	Santander IE	LC06	SDN0031	1701	16200	Roissy CDG	XR Headset	Customer is looking to purchase XR Headset to innovate training programs.	UK	Birmingham	Tech Sourcing
                5	Audi AG	Audi AG UK	LC30	SDN0032	366	1325	Doha	Consulting Package	Customer is looking to purchase Consulting Package for strategic planning.	DE	Frankfurt	Consulting
                6	AOK Plus	AOK Plus US	LC31	SDN0033	4556	1542	Moordrecht	Managed Services	Customer is looking to purchase Managed Services to improve IT operations.	JP	Kyoto	Managed Service
                1	Santander	Santander BR	LC20	SDN0034	908	16817	Roissy CDG	XR Headset	Customer is looking to purchase XR Headset to innovate training programs.	JP	Tokyo	Tech Sourcing";

            string[] lines = csvData.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<Order> orders = new List<Order>();

            foreach (var line in lines)
            {
                var values = line.Split('\t');

                if (values.Length < 13)
                {
                    // Handle the error or log it
                    Console.WriteLine("Error: Line does not contain enough values.");
                    continue;
                }

                orders.Add(new Order
                {
                    ParentCustomerID = int.Parse(values[0]),
                    ParentCustomer = values[1],
                    LocalCustomer = values[2],
                    LocalCustomerID = values[3],
                    SalesOrderID = values[4],
                    UnitQuantity = int.Parse(values[5]),
                    ItemValue = decimal.Parse(values[6]),
                    StorageLocation = values[7],
                    OrderItemName = values[8],
                    OrderItemDescription = values[9],
                    ShippingDestinationCountry = values[10],
                    ShippingDestinationCity = values[11],
                    ProfitCentre = values[12]
                });
            }

            if (!context.Orders.Any())
            {
                context.Orders.AddRange(orders);
                context.SaveChanges();
            }
        }

        public static void SeedOrderHeaders(OrderHeaderDb context)
        {

            string csvData = @"100002	SDN0001	Standard Order	Consulting 	Direct Sales	3	BMW Group	LC03	BMW Group UK	3/22/2024	4/14/2024
                100002	SDN0002	Rush Order	Managed Services 	Nearshore and Offshore Operations	3	BMW Group	LC03	BMW Group UK	4/6/2024	4/29/2024
                100003	SDN0003	Cash Sale	Tech Sourcing 	Service Centers	1	Santander	LC04	Santander DE	4/13/2024	5/6/2024
                100050	SDN0004	Returns	Tech Sourcing 	Integration Centers	6	AOK Plus	LC31	AOK Plus US	8/4/2024	8/27/2024
                100023	SDN0005	Credit Memo Request 	Tech Sourcing 	e-Commerce	1	Santander	LC20	Santander BR	8/11/2024	9/3/2024
                100003	SDN0006	Debit Memo Request 	Tech Sourcing 	SalesBot	1	Santander	LC04	Santander DE	9/2/2024	9/25/2024
                100048	SDN0007	Standard Order	Tech Sourcing 	Channel Partners	5	Audi AG	LC30	Audi AG UK	9/7/2024	9/30/2024
                100019	SDN0008	Rush Order	Tech Sourcing 	Telemarketing	3	BMW Group	LC16	BMW Group IE	11/4/2024	11/27/2024
                100009	SDN0009	Cash Sale	Tech Sourcing 	Content Marketing	2	Bosch	LC09	Bosch FR	12/9/2023	1/1/2024
                100017	SDN0010	Returns	Tech Sourcing 	Direct Sales	1	Santander	LC04	Santander DE	12/24/2023	1/16/2024
                100017	SDN0011	Credit Memo Request 	Managed Services 	e-Commerce	1	Santander	LC04	Santander DE	1/8/2024	1/31/2024
                100044	SDN0012	Cash Sale	Managed Services 	e-Commerce	2	Bosch	LC28	Bosch DE	1/23/2024	2/15/2024
                100015	SDN0013	Standard Order	Managed Services 	Industry Conferences	6	AOK Plus	LC14	AOK Plus FR	2/7/2024	3/1/2024
                100050	SDN0014	Credit Memo Request 	Managed Services 	Channel Partners	6	AOK Plus	LC31	AOK Plus US	2/22/2024	3/16/2024
                100023	SDN0015	Debit Memo Request 	Managed Services 	Telemarketing	1	Santander	LC20	Santander BR	3/8/2024	3/31/2024
                100003	SDN0016	Rush Order	Managed Services 	Content Marketing	1	Santander	LC04	Santander DE	3/22/2024	4/14/2024
                100048	SDN0017	Cash Sale	Managed Services 	Direct Sales	5	Audi AG	LC30	Audi AG UK	4/6/2024	4/29/2024
                100019	SDN0018	Standard Order	Managed Services 	Channel Partners	3	BMW Group	LC16	BMW Group IE	8/11/2024	9/3/2024
                100045	SDN0019	Credit Memo Request 	Managed Services 	Distributors	3	BMW Group	LC29	BMW Group JP	9/2/2024	9/25/2024
                100009	SDN0020	Debit Memo Request 	Consulting 	Technology Alliances	2	Bosch	LC09	Bosch FR	9/7/2024	9/30/2024
                100009	SDN0021	Standard Order	Consulting 	Referral Programs	2	Bosch	LC09	Bosch FR	11/4/2024	11/27/2024
                100017	SDN0022	Rush Order	Consulting 	Distributors	1	Santander	LC04	Santander DE	12/9/2023	1/1/2024
                100017	SDN0023	Cash Sale	Consulting 	Direct Sales	1	Santander	LC04	Santander DE	12/24/2023	1/16/2024
                100044	SDN0024	Standard Order	Consulting 	Technology Alliances	2	Bosch	LC28	Bosch DE	1/8/2024	1/31/2024
                100015	SDN0025	Credit Memo Request 	Consulting 	Telemarketing	6	AOK Plus	LC14	AOK Plus FR	1/23/2024	2/15/2024
                100043	SDN0026	Cash Sale	Consulting 	Industry Conferences	2	Bosch	LC28	Bosch DE	1/8/2024	1/31/2024
                100043	SDN0027	Standard Order	Consulting 	E-commerce Websites	2	Bosch	LC28	Bosch DE	1/23/2024	2/15/2024
                100043	SDN0028	Credit Memo Request 	Consulting 	Referral Programs	2	Bosch	LC28	Bosch DE	10/17/2024	11/9/2024
                100024	SDN0029	Debit Memo Request 	Consulting 	E-commerce Websites	1	Santander	LC20	Santander BR	11/1/2024	11/24/2024
                100048	SDN0030	Debit Memo Request 	Consulting 	Mobile Apps	5	Audi AG	LC30	Audi AG UK	11/16/2024	12/9/2024
                100051	SDN0031	Standard Order	Consulting 	Distributors	1	Santander	LC06	Santander IE	12/1/2024	12/24/2024
                100047	SDN0032	Rush Order	Consulting 	Direct Sales	5	Audi AG	LC30	Audi AG UK	12/16/2024	1/8/2025
                100050	SDN0033	Cash Sale	Managed Services 	Industry Conferences	6	AOK Plus	LC31	AOK Plus US	12/31/2024	1/23/2025
                100023	SDN0034	Credit Memo Request 	Managed Services 	Technology Alliances	1	Santander	LC20	Santander BR	2/22/2024	3/16/2024
                100003	SDN0035	Credit Memo Request 	Consulting 	E-commerce Websites	1	Santander	LC04	Santander DE	3/8/2024	3/31/2024
                100048	SDN0036	Cash Sale	Consulting 	Technology Alliances	5	Audi AG	LC30	Audi AG UK	11/4/2024	11/27/2024
                100019	SDN0037	Cash Sale	Consulting 	Technology Alliances	3	BMW Group	LC16	BMW Group IE	12/9/2023	1/1/2024
                100045	SDN0038	Standard Order	Consulting 	Channel Partners	3	BMW Group	LC29	BMW Group JP	12/24/2023	1/16/2024
                100009	SDN0039	Credit Memo Request 	Tech Sourcing 	Distributors	2	Bosch	LC09	Bosch FR	1/8/2024	1/31/2024
                100047	SDN0040	Debit Memo Request 	Tech Sourcing 	Channel Partners	5	Audi AG	LC30	Audi AG UK	1/23/2024	2/15/2024
                100050	SDN0041	Rush Order	Tech Sourcing 	Telemarketing	6	AOK Plus	LC31	AOK Plus US	1/8/2024	1/31/2024
                100023	SDN0042	Cash Sale	Tech Sourcing 	Content Marketing	1	Santander	LC20	Santander BR	1/23/2024	2/15/2024
                100003	SDN0043	Returns	Tech Sourcing 	Direct Sales	1	Santander	LC04	Santander DE	10/17/2024	11/9/2024
                100045	SDN0044	Debit Memo Request 	Managed Services 	Telemarketing	3	BMW Group	LC29	BMW Group JP	11/1/2024	11/24/2024
                100009	SDN0045	Standard Order	Managed Services 	Channel Partners	2	Bosch	LC09	Bosch FR	11/16/2024	12/9/2024
                100009	SDN0046	Rush Order	Managed Services 	Agents/Brokers	2	Bosch	LC09	Bosch FR	12/1/2024	12/24/2024
                100017	SDN0047	Cash Sale	Managed Services 	E-commerce Websites	1	Santander	LC04	Santander DE	3/8/2024	3/31/2024
                100017	SDN0048	Standard Order	Consulting 	Telemarketing	1	Santander	LC04	Santander DE	11/4/2024	11/27/2024
                100044	SDN0049	Standard Order	Consulting 	Channel Partners	2	Bosch	LC28	Bosch DE	12/9/2023	1/1/2024
                100015	SDN0050	Debit Memo Request 	Consulting 	Mobile Apps	6	AOK Plus	LC14	AOK Plus FR	12/24/2023	1/16/2024";

            string[] lines = csvData.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<OrderHeader> orderHeaders = new List<OrderHeader>();

            foreach (var line in lines)
            {
                string[] values = line.Split('\t');

                if (values.Length < 11)
                {
                    // Handle the error or log it
                    Console.WriteLine("Error: Line does not contain enough values.");
                    continue;
                }

                OrderHeader orderHeader = new OrderHeader
                {
                    OpportunityID = int.Parse(values[0]),
                    SalesDocNumber = values[1],
                    SalesDocType = values[2],
                    SalesOrganisation = values[3],
                    DistributionChannel = values[4],
                    ParentCustomerID = int.Parse(values[5]),
                    ParentCustomer = values[6],
                    LocalCustomerID = values[7],
                    LocalCustomer = values[8],
                    DateCreated = DateTime.ParseExact(values[9], "M/d/yyyy", CultureInfo.InvariantCulture),
                    CreatedBy = values[10]
                };
                orderHeaders.Add(orderHeader);
            }

            if (!context.OrderHeaders.Any())
            {
                context.OrderHeaders.AddRange(
                    orderHeaders
                );
                context.SaveChanges();
            }
        }


        private static void SeedInvoices(InvoiceDb context)
        {
            if (!context.Invoices.Any())
            {
                string csvData = @"100002	SDN0001	INVOIC0001	3	BMW Group	LC03	BMW Group UK	14/04/2024	17/05/2024	£	1735066
                    100002	SDN0002	INVOIC0002	3	BMW Group	LC03	BMW Group UK	29/04/2024	01/06/2024	£	1763609
                    100003	SDN0003	INVOIC0003	1	Santander	LC04	Santander DE	06/05/2024	07/06/2024	€	689230
                    100050	SDN0004	INVOIC0004	6	AOK Plus	LC31	AOK Plus US	27/08/2024	26/09/2024	$	1074105
                    100023	SDN0005	INVOIC0005	1	Santander	LC20	Santander BR	03/09/2024	05/10/2024	R$	230448
                    100003	SDN0006	INVOIC0006	1	Santander	LC04	Santander DE	25/09/2024	21/10/2024	€	904065
                    100048	SDN0007	INVOIC0007	5	Audi AG	LC30	Audi AG UK	30/09/2024	30/10/2024	£	2660260
                    100019	SDN0008	INVOIC0008	3	BMW Group	LC16	BMW Group IE	27/11/2024	N/A	€	2990224
                    100009	SDN0009	INVOIC0009	2	Bosch	LC09	Bosch FR	01/01/2024	28/01/2024	€	1334916
                    100017	SDN0010	INVOIC0010	1	Santander	LC04	Santander DE	16/01/2024	11/02/2024	€	966461
                    100017	SDN0011	INVOIC0011	1	Santander	LC04	Santander DE	31/01/2024	03/03/2024	€	1689308
                    100044	SDN0012	INVOIC0012	2	Bosch	LC28	Bosch DE	15/02/2024	15/03/2024	€	2829691
                    100015	SDN0013	INVOIC0013	6	AOK Plus	LC14	AOK Plus FR	01/03/2024	31/03/2024	€	2624999
                    100050	SDN0014	INVOIC0014	6	AOK Plus	LC31	AOK Plus US	16/03/2024	19/04/2024	$	291998
                    100023	SDN0015	INVOIC0015	1	Santander	LC20	Santander BR	31/03/2024	29/04/2024	R$	1304135
                    100003	SDN0016	INVOIC0016	1	Santander	LC04	Santander DE	14/04/2024	18/05/2024	€	1728758
                    100048	SDN0017	INVOIC0017	5	Audi AG	LC30	Audi AG UK	29/04/2024	25/05/2024	£	453860
                    100019	SDN0018	INVOIC0018	3	BMW Group	LC16	BMW Group IE	03/09/2024	06/10/2024	€	2361331
                    100045	SDN0019	INVOIC0019	3	BMW Group	LC29	BMW Group JP	25/09/2024	26/10/2024	¥	713908
                    100009	SDN0020	INVOIC0020	2	Bosch	LC09	Bosch FR	30/09/2024	N/A	€	2276078
                    100009	SDN0021	INVOIC0021	2	Bosch	LC09	Bosch FR	27/11/2024	N/A	€	2505627
                    100017	SDN0022	INVOIC0022	1	Santander	LC04	Santander DE	01/01/2024	26/01/2024	€	506441
                    100017	SDN0023	INVOIC0023	1	Santander	LC04	Santander DE	16/01/2024	11/02/2024	€	2428690
                    100044	SDN0024	INVOIC0024	2	Bosch	LC28	Bosch DE	31/01/2024	01/03/2024	€	1050879
                    100015	SDN0025	INVOIC0025	6	AOK Plus	LC14	AOK Plus FR	15/02/2024	21/03/2024	€	1917278
                    100043	SDN0026	INVOIC0026	2	Bosch	LC28	Bosch DE	31/01/2024	04/03/2024	€	15007
                    100043	SDN0027	INVOIC0027	2	Bosch	LC28	Bosch DE	15/02/2024	17/03/2024	€	116645
                    100043	SDN0028	INVOIC0028	2	Bosch	LC28	Bosch DE	09/11/2024	N/A	€	371173
                    100024	SDN0029	INVOIC0029	1	Santander	LC20	Santander BR	24/11/2024	N/A	R$	1319176
                    100048	SDN0030	INVOIC0030	5	Audi AG	LC30	Audi AG UK	09/12/2024	11/01/2025	£	1269687
                    100051	SDN0031	INVOIC0031	1	Santander	LC06	Santander IE	24/12/2024	22/01/2025	€	640325
                    100047	SDN0032	INVOIC0032	5	Audi AG	LC30	Audi AG UK	08/01/2025	05/02/2025	£	408110
                    100050	SDN0033	INVOIC0033	6	AOK Plus	LC31	AOK Plus US	23/01/2025	25/02/2025	$	2664062
                    100023	SDN0034	INVOIC0034	1	Santander	LC20	Santander BR	16/03/2024	19/04/2024	R$	1687293
                    100003	SDN0035	INVOIC0035	1	Santander	LC04	Santander DE	31/03/2024	25/04/2024	€	2204142
                    100048	SDN0036	INVOIC0036	5	Audi AG	LC30	Audi AG UK	27/11/2024	N/A	£	2866935
                    100019	SDN0037	INVOIC0037	3	BMW Group	LC16	BMW Group IE	01/01/2024	01/02/2024	€	1225847
                    100045	SDN0038	INVOIC0038	3	BMW Group	LC29	BMW Group JP	16/01/2024	11/02/2024	¥	1155587
                    100009	SDN0039	INVOIC0039	2	Bosch	LC09	Bosch FR	31/01/2024	29/02/2024	€	1906079
                    100047	SDN0040	INVOIC0040	5	Audi AG	LC30	Audi AG UK	15/02/2024	12/03/2024	£	511997
                    100050	SDN0041	INVOIC0041	6	AOK Plus	LC31	AOK Plus US	31/01/2024	05/03/2024	$	872767
                    100023	SDN0042	INVOIC0042	1	Santander	LC20	Santander BR	15/02/2024	14/03/2024	R$	868548
                    100003	SDN0043	INVOIC0043	1	Santander	LC04	Santander DE	09/11/2024	N/A	€	1928289
                    100045	SDN0044	INVOIC0044	3	BMW Group	LC29	BMW Group JP	24/11/2024	N/A	¥	2511379
                    100009	SDN0045	INVOIC0045	2	Bosch	LC09	Bosch FR	09/12/2024	N/A	€	2802556
                    100009	SDN0046	INVOIC0046	2	Bosch	LC09	Bosch FR	24/12/2024	N/A	€	2385514
                    100017	SDN0047	INVOIC0047	1	Santander	LC04	Santander DE	31/03/2024	N/A	€	2286047
                    100017	SDN0048	INVOIC0048	1	Santander	LC04	Santander DE	27/11/2024	N/A	€	1217756
                    100044	SDN0049	INVOIC0049	2	Bosch	LC28	Bosch DE	01/01/2024	29/02/2024	€	780978
                    100015	SDN0050	INVOIC0050	6	AOK Plus	LC14	AOK Plus FR	16/01/2024	N/A	€	2788355
                    ";

                string[] lines = csvData.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                List<Invoice> invoices = new List<Invoice>();

                foreach (var line in lines)
                {
                    string[] values = line.Split('\t').Select(v => v.Trim()).ToArray();

                    if (values.Length < 11)
                    {
                        // Handle the error or log it
                        Console.WriteLine("Error: Line does not contain enough values.");
                        continue;
                    }

                    try
                    {
                        DateTime? paymentDate = null;
                        if (!string.IsNullOrEmpty(values[8]) && values[8] != "N/A")
                        {
                            paymentDate = DateTime.ParseExact(values[8], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }

                        invoices.Add(new Invoice
                        {
                            SalesDocNumber = values[1],
                            InvoiceNumber = values[2],
                            ParentCustomerID = int.Parse(values[3]),
                            ParentCustomer = values[4],
                            LocalCustomerID = values[5],
                            LocalCustomer = values[6],
                            InvoiceDate = DateTime.ParseExact(values[7], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            PaymentDate = paymentDate,
                            Currency = values[9],
                            TotalAmount = decimal.Parse(values[10], CultureInfo.InvariantCulture)
                        });
                    }
                    catch (Exception ex)
                    {
                        // Log the error and continue with the next line
                        Console.WriteLine($"Error parsing line: {line}. Exception: {ex.Message}");
                    }
                }

                context.Invoices.AddRange(invoices);
                context.SaveChanges();
            }
        }


        private static void SeedOpportunities2(OpportunityDb context)
        {
            string csvData = @"Santander - Device refresh program	Customer  has an ageing estate of end user devices that need to be refreshed in the next 3-6 months	100000	Santander	1	Santander UK	UK	EU	Tech Sourcing	0.75	Quote	3750000	£	Adebayo.Adeyemi@CCSpark.io	02/12/2024	31/12/2024
                Bosch - Device refresh program	Customer  has an ageing estate of end user devices that need to be refreshed in the next 3-6 months	100001	Bosch	2	Bosch UK	UK	EU	Tech Sourcing	0.5	Quote	750000	£	Jacob.Harris@CCSpark.io	09/12/2024	31/12/2024
                Wingtip Toys  - Device refresh program	Customer  has an ageing estate of end user devices that need to be refreshed in the next 3-6 months	100002	BMW Group	3	BMW Group UK	UK	EU	Tech Sourcing	1	Quote	1500000	£	Adebayo.Adeyemi@CCSpark.io	04/11/2024	31/12/2024
                Zero-Touch Provisioning for Corporate Devices:	Implement automated enrollment options for corporate devices to streamline the provisioning process and reduce manual intervention	100003	Santander	1	Santander DE	DE	EU	Managed Service	0.25	Qualification	234000	£	Jacob.Harris@CCSpark.io	09/12/2023	01/01/2024
                BYOD Enrollment and Management:	Provide a self-service Company Portal for users to enroll their BYOD devices, ensuring secure access to corporate resources	100004	Santander	1	Santander Group	DE	EU	Tech Sourcing	0.45	Qualification	234110	£	Jacob.Harris@CCSpark.io	24/12/2023	16/01/2024
                Custom Terms and Conditions at Enrollment	Deliver custom terms and conditions during device enrollment to ensure compliance with company policies	100005	Santander	1	Santander DE	DE	EU	Consulting	0.5	Prospecting	34000	£	Amanda.King@CCSpark.io	08/01/2024	31/01/2024
                Remote Assistance for Device Support	Offer remote assistance to troubleshoot and resolve device issues, enhancing user satisfaction and reducing downtime	100006	Santander	1	Santander IE	IE	EU	Consulting	1	Closed Won	432502	£	Adebayo.Adeyemi@CCSpark.io	23/01/2024	15/02/2024
                Selective Wipe for Lost or Stolen Devices	Perform selective wipes on lost or stolen devices to protect corporate data while preserving personal information	100007	Santander	1	Santander FR	US 	US	Managed Service	1	Closed Won	1002303	£	Adebayo.Adeyemi@CCSpark.io	07/02/2024	01/03/2024
                Deploy Device Security Policies	Implement and enforce device security policies to protect corporate data and ensure compliance with security standards	100008	Bosch	2	Bosch IE	US	US	Managed Service	0.7	Qualification	3458245	£	Liu.Wei@CCSpark.io	22/02/2024	16/03/2024
                Install Mandatory Apps	Deploy mandatory applications to devices to ensure users have the necessary tools for their roles	100009	Bosch	2	Bosch FR	FR	EU	Tech Sourcing	0.5	Qualification	4739792	£	Emily.Johnson@CCSpark.io	08/03/2024	31/03/2024
                Restrict Access to Corporate Resources: 	Restrict access to corporate resources if device policies are violated, such as using a jailbroken device	100010	Bosch	2	Bosch UK	UK	EU	Tech Sourcing	0.5	Quote	3175331	£	Emily.Johnson@CCSpark.io	22/03/2024	14/04/2024
                Protect Corporate Data	Protect corporate data by restricting actions such as copy/cut/paste/save outside of the managed app ecosystem	100011	BMW Group	3	BMW Group US	US	US	Managed Service	0.45	Quote	4019959	£	Thomas.Turner@CCSpark.io	06/04/2024	29/04/2024
                Report on Device and App Compliance	Generate reports on device and app compliance to monitor adherence to security policies	100012	BMW Group	3	BMW Group DE	DE	EU	Consulting	0.7	Closed Lost	424884	£	Liu.Wei@CCSpark.io	21/04/2024	14/05/2024
                Employee Device Refresh Program	: Implement a device refresh program to ensure employees have up-to-date and efficient devices for their work	100013	Audi AG	5	Audi AG DE	DE	EU	Consulting	0.3	Quote	1702300	£	Thomas.Turner@CCSpark.io	04/08/2024	27/08/2024
                New Hire Device Provisioning:	Provide new hires with the necessary devices and equipment to ensure a smooth onboarding process	100014	Audi AG	5	Audi AG IE	IE	EU	Tech Sourcing	0.3	Quote	5382643	£	Emily.Johnson@CCSpark.io	18/08/2024	10/09/2024
                Primary Device Replacements	Manage primary device replacements for employees, ensuring minimal disruption to their work	100015	AOK Plus	6	AOK Plus FR	FR	EU	Managed Service	0.8	Quote	616384	£	Thomas.Turner@CCSpark.io	02/09/2024	25/09/2024
                Surface Device Management	 Offer comprehensive management of Surface devices, including deployment, security, and support	100016	AOK Plus	6	AOK Plus IE	IE	EU	Managed Service	0.6	Quote	8530913	£	Jacob.Harris@CCSpark.io	17/09/2024	10/10/2024
                Enhanced Lifecycle Management for D365	Assist customers with application enhancements, platform updates, and ALM best practices using Azure DevOps	100017	Santander	1	Santander DE	DE	EU	Managed Service	0.2	Closed Lost	2909680	£	Jacob.Harris@CCSpark.io	02/10/2024	25/10/2024
                Supplier Lifecycle Management	: Implement a supplier lifecycle management process to ensure compliance and manage supplier relationships effectively	100018	BMW Group	3	BMW Group DE	DE	EU	Consulting	0.5	Quote	6622684	£	Adebayo.Adeyemi@CCSpark.io	17/10/2024	09/11/2024
                Device Inventory Management	Track and manage device inventory to ensure optimal utilization and reduce costs	100019	BMW Group	3	BMW Group IE	IE	EU	Tech Sourcing	1	Quote	2737663	£	Jessica.Thomas@CCSpark.io	01/11/2024	24/11/2024
                Device Decommissioning and Disposal	 Manage the decommissioning and disposal of devices to ensure data security and environmental compliance	100020	Bosch	2	Bosch FR	FR	EU	Consulting	1	Closed Won	3581566	£	Jessica.Thomas@CCSpark.io	16/11/2024	09/12/2024
                Device Maintenance and Support	Provide ongoing maintenance and support for devices to ensure they remain in optimal working condition	100021	Bosch	2	Bosch UK	UK	EU	Managed Service	1	Closed Won	2263141	£	Adebayo.Adeyemi@CCSpark.io	01/12/2024	24/12/2024
                Device Compliance and Security Audits	Conduct regular compliance and security audits to identify and address potential vulnerabilities	100022	Telefonica	7	Telefonica UK	UK	EU	Consulting	0.5	Quote	5464675	£	Emily.Johnson@CCSpark.io	16/12/2024	08/01/2025
                IT Infrastructure Consulting	Comprehensive consulting to design, implement, and manage IT infrastructure, ensuring optimal performance and scalability.	100023	Santander	1	Santander BR	BR	LATAM	Consulting	0.25	Quote	1185701	£	Emily.Johnson@CCSpark.io	31/12/2024	23/01/2025
                Cybersecurity Assessment	In-depth assessment of cybersecurity measures to identify vulnerabilities and recommend improvements.	100024	Santander	1	Santander BR	BR	LATAM	Consulting	0.45	Quote	8908635	£	Jacob.Harris@CCSpark.io	22/02/2024	16/03/2024
                Cloud Migration Services	Assistance with migrating data and applications to the cloud, ensuring minimal disruption and maximum efficiency.	100025	Santander	1	Santander DE	DE	EU	Consulting	0.5	Quote	937899	£	Jacob.Harris@CCSpark.io	08/03/2024	31/03/2024
                Data Center Optimization	Optimization of data center operations to enhance performance, reduce costs, and improve energy efficiency.	100026	Santander	1	Santander DE	DE	EU	Consulting	1	Quote	4206898	£	Megan.Hernandez@CCSpark.io	22/03/2024	14/04/2024
                Network Design Consulting	Expert consulting on designing robust and scalable network architectures tailored to business needs.	100027	Bosch	2	Bosch IE	IE	EU	Consulting	1	Quote	2096035	£	Adebayo.Adeyemi@CCSpark.io	06/04/2024	29/04/2024
                IT Strategy Consulting	Strategic consulting to align IT initiatives with business goals, ensuring long-term success and innovation.	100028	Bosch	2	Bosch FR	FR	EU	Consulting	0.7	Quote	2338025	£	Amanda.King@CCSpark.io	13/04/2024	06/05/2024
                Application Modernization	Modernization of legacy applications to improve functionality, performance, and user experience.	100029	Bosch	2	Bosch IE	IE	EU	Consulting	0.5	Quote	2791488	£	Liu.Wei@CCSpark.io	04/08/2024	27/08/2024
                Digital Transformation	Comprehensive services to drive digital transformation, enhancing business processes and customer engagement.	100030	BMW Group	3	BMW Group SA	SA	AFRICA	Consulting	0.5	Quote	2170016	£	Amanda.King@CCSpark.io	11/08/2024	03/09/2024
                Managed IT Services	Ongoing management and support of IT infrastructure and services, ensuring reliability and efficiency.	100031	BMW Group	3	BMW Group SA	SA	AFRICA	Consulting	0.45	Quote	1059580	£	Amanda.King@CCSpark.io	02/09/2024	25/09/2024
                Business Continuity Planning	Development of business continuity plans to ensure operations can continue during and after a disaster.	100032	Audi AG	5	Audi AG IE	IE	EU	Consulting	0.7	Quote	2120210	£	Liu.Wei@CCSpark.io	07/09/2024	30/09/2024
                IT Governance Consulting	Consulting on establishing IT governance frameworks to ensure compliance and effective management of IT resources.	100033	Audi AG	5	Audi AG FR	FR	EU	Consulting	30	Quote	8340861	£	Liu.Wei@CCSpark.io	04/11/2024	27/11/2024
                Enterprise Architecture	Design and implementation of enterprise architecture to support business strategy and operations.	100034	AOK Plus	6	AOK Plus UK	UK	EU	Consulting	30	Quote	8358724	£	Thomas.Turner@CCSpark.io	09/12/2023	01/01/2024
                IT Service Management	Implementation of IT service management practices to improve service delivery and customer satisfaction.	100035	AOK Plus	6	AOK Plus UK	UK	EU	Consulting	90	Prospecting	3492480	£	Adebayo.Adeyemi@CCSpark.io	24/12/2023	16/01/2024
                Cloud Security Consulting	Consulting on securing cloud environments, protecting data, and ensuring compliance with regulations.	100036	Santander	1	Santander UK	UK	EU	Consulting	0.5	Prospecting	50726	£	Adebayo.Adeyemi@CCSpark.io	08/01/2024	31/01/2024
                DevOps Transformation	Services to implement DevOps practices, improving collaboration, and accelerating software delivery.	100037	BMW Group	3	BMW Group DE	DE	EU	Consulting	0.45	Qualification	1857088	£	Adebayo.Adeyemi@CCSpark.io	23/01/2024	15/02/2024
                IT Risk Management	Identification and management of IT risks to protect business assets and ensure continuity.	100038	Santander	1	Santander DE	DE	EU	Consulting	0.7	Qualification	612207	£	Thomas.Turner@CCSpark.io	07/02/2024	01/03/2024
                Software Development	Custom software development services to meet specific business requirements and enhance operations.	100039	Santander	1	Santander DE	DE	EU	Consulting	30	Qualification	8548486	£	Thomas.Turner@CCSpark.io	22/02/2024	16/03/2024
                IT Compliance Consulting	Consulting on achieving and maintaining compliance with industry regulations and standards.	100040	Santander	1	Santander IE	IE	EU	Consulting	30	Qualification	2430206	£	Thomas.Turner@CCSpark.io	08/03/2024	31/03/2024
                IT Asset Management	Management of IT assets to optimize usage, reduce costs, and ensure compliance.	100041	Santander	1	Santander FR	FR	EU	Consulting	90	Qualification	1423898	£	Jessica.Thomas@CCSpark.io	22/03/2024	14/04/2024
                IT Operations Consulting	Consulting on improving IT operations, enhancing efficiency, and reducing downtime.	100042	Bosch	2	Bosch IT	IT	EU	Consulting	60	Qualification	4991963	£	Megan.Hernandez@CCSpark.io	06/04/2024	29/04/2024
                IT Project Management	Project management services to ensure successful delivery of IT projects on time and within budget.	100043	Bosch	2	Bosch DE	DE	EU	Consulting	0.25	Qualification	4454482	£	Megan.Hernandez@CCSpark.io	11/08/2024	03/09/2024
                IT Vendor Management	Management of IT vendors to ensure quality service delivery and cost-effectiveness.	100044	Bosch	2	Bosch DE	DE	EU	Consulting	0.45	Qualification	293600	£	Jessica.Thomas@CCSpark.io	02/09/2024	25/09/2024
                IT Outsourcing Consulting	Consulting on outsourcing IT services to improve efficiency and reduce costs.	100045	BMW Group	3	BMW Group JP	JP	ASIA	Consulting	0.5	Prospecting	726522	£	Megan.Hernandez@CCSpark.io	07/09/2024	30/09/2024
                IT Cost Optimization	Services to optimize IT costs, ensuring maximum value from IT investments.	100046	BMW Group	3	BMW Group JP	JP	ASIA	Consulting	1	Prospecting	1371362	£	Megan.Hernandez@CCSpark.io	04/11/2024	27/11/2024
                IT Performance Management	Monitoring and management of IT performance to ensure optimal operation and service delivery.	100047	Audi AG	5	Audi AG UK	UK	EU	Consulting	1	Prospecting	3066779	£	Samantha.Allen@CCSpark.io	09/12/2023	01/01/2024
                IT Process Improvement	Consulting on improving IT processes to enhance efficiency and effectiveness.	100048	Audi AG	5	Audi AG UK	UK	EU	Consulting	0.7	Prospecting	3264414	£	Adebayo.Adeyemi@CCSpark.io	24/12/2023	16/01/2024
                IT Infrastructure Audit	Comprehensive audit of IT infrastructure to identify areas for improvement and ensure compliance.	100049	AOK Plus	6	AOK Plus US	US	US	Consulting	0.5	Prospecting	8186015	£	Adebayo.Adeyemi@CCSpark.io	08/01/2024	31/01/2024
                IT Training Services	Training services to enhance the skills and knowledge of IT staff.	100050	AOK Plus	6	AOK Plus US	US	US	Consulting	0.5	Prospecting	7835765	£	Samantha.Allen@CCSpark.io	23/01/2024	15/02/2024
                IT Support Services	Ongoing support services to ensure the smooth operation of IT systems and resolve issues promptly.	100051	Santander	1	Santander IE	IE	EU	Consulting	1	Quote	6759459	£	Liu.Wei@CCSpark.io	08/01/2024	31/01/2024
                IT Staffing Services	Staffing services to provide skilled IT professionals for short-term or long-term projects.	100052	BMW Group	3	BMW Group DE	DE	EU	Consulting	0.7	Prospecting	7129885	£	Jessica.Thomas@CCSpark.io	23/01/2024	15/02/2024
                ";

            string[] lines = csvData.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<Opportunity> opportunities = new List<Opportunity>();

            foreach (var line in lines)
            {
                string[] values = line.Split('\t').Select(v => v.Trim()).ToArray();

                if (values.Length < 16)
                {
                    // Handle the error or log it
                    Console.WriteLine("Error: Line does not contain enough values.");
                    continue;
                }

                try
                {
                    opportunities.Add(new Opportunity
                    {
                        Name = values[0].Trim(),
                        Description = values[1],
                        ParentAccountId = int.Parse(values[4]),
                        ParentAccount = values[3],
                        OpportunityID = values[2],
                        Account = values[5],
                        Territory = values[6],
                        Region = values[7],
                        ServiceLine = values[8],
                        Probability = double.Parse(values[9]),
                        StageName = values[10],
                        Amount = decimal.Parse(values[11], System.Globalization.CultureInfo.InvariantCulture),
                        Currency = values[12],
                        Owner = values[13],
                        DateCreated = DateTime.Parse(values[14], new System.Globalization.CultureInfo("en-GB")),
                        CloseDate = DateTime.Parse(values[15], new System.Globalization.CultureInfo("en-GB"))
                    });
                }
                catch (Exception ex)
                {
                    // Log the error and continue with the next line
                    Console.WriteLine($"Error parsing line: {line}. Exception: {ex.Message}");
                }
            }

            context.Opportunities.AddRange(opportunities);
            context.SaveChanges();
        }

        private static void SeedOpportunities(OpportunityDb context)
        {
            if (!context.Opportunities.Any())
            {
                var opportunities = new List<Opportunity>
                {
                    new Opportunity
                    {
                        Name = "Opportunity 1",
                        Description = "Description for Opportunity 1",
                        ParentAccountId = 1,
                        ParentAccount = "Parent Account 1",
                        OpportunityID = "OPP001",
                        Account = "Account 1",
                        Territory = "Territory 1",
                        Region = "Region 1",
                        ServiceLine = "Service Line 1",
                        Probability = 80,
                        StageName = "Stage 1",
                        Amount = 10000m,
                        Currency = "USD",
                        Owner = "Owner 1",
                        DateCreated = DateTime.Parse("2023-01-01"),
                        CloseDate = DateTime.Parse("2023-06-01")
                    },
                    new Opportunity
                    {
                        Name = "Opportunity 2",
                        Description = "Description for Opportunity 2",
                        ParentAccountId = 2,
                        ParentAccount = "Parent Account 2",
                        OpportunityID = "OPP002",
                        Account = "Account 2",
                        Territory = "Territory 2",
                        Region = "Region 2",
                        ServiceLine = "Service Line 2",
                        Probability = 70,
                        StageName = "Stage 2",
                        Amount = 20000m,
                        Currency = "USD",
                        Owner = "Owner 2",
                        DateCreated = DateTime.Parse("2023-02-01"),
                        CloseDate = DateTime.Parse("2023-07-01")
                    },
                    new Opportunity
                    {
                        Name = "Opportunity 3",
                        Description = "Description for Opportunity 3",
                        ParentAccountId = 3,
                        ParentAccount = "Parent Account 3",
                        OpportunityID = "OPP003",
                        Account = "Account 3",
                        Territory = "Territory 3",
                        Region = "Region 3",
                        ServiceLine = "Service Line 3",
                        Probability = 90,
                        StageName = "Stage 3",
                        Amount = 30000m,
                        Currency = "USD",
                        Owner = "Owner 3",
                        DateCreated = DateTime.Parse("2023-03-01"),
                        CloseDate = DateTime.Parse("2023-08-01")
                    },
                    new Opportunity
                    {
                        Name = "Opportunity 4",
                        Description = "Description for Opportunity 4",
                        ParentAccountId = 4,
                        ParentAccount = "Parent Account 4",
                        OpportunityID = "OPP004",
                        Account = "Account 4",
                        Territory = "Territory 4",
                        Region = "Region 4",
                        ServiceLine = "Service Line 4",
                        Probability = 60,
                        StageName = "Stage 4",
                        Amount = 40000m,
                        Currency = "USD",
                        Owner = "Owner 4",
                        DateCreated = DateTime.Parse("2023-04-01"),
                        CloseDate = DateTime.Parse("2023-09-01")
                    },
                    new Opportunity
                    {
                        Name = "Opportunity 5",
                        Description = "Description for Opportunity 5",
                        ParentAccountId = 5,
                        ParentAccount = "Parent Account 5",
                        OpportunityID = "OPP005",
                        Account = "Account 5",
                        Territory = "Territory 5",
                        Region = "Region 5",
                        ServiceLine = "Service Line 5",
                        Probability = 50,
                        StageName = "Stage 5",
                        Amount = 50000m,
                        Currency = "USD",
                        Owner = "Owner 5",
                        DateCreated = DateTime.Parse("2023-05-01"),
                        CloseDate = DateTime.Parse("2023-10-01")
                    }
                };

                context.Opportunities.AddRange(opportunities);
                context.SaveChanges();
            }
        }

        private static void SeedCustomers(CustomerDb context)
        {
            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                    new Customer { Id = 1, Name = "Santander", AccountOwner = "Amanda.Green@CCSpark.io", SellerID = 14 },
                    new Customer { Id = 2, Name = "Bosch", AccountOwner = "Emily.Clark@CCSpark.io", SellerID = 32 },
                    new Customer { Id = 3, Name = "BMW Group", AccountOwner = "Kimberly.Nelson@CCSpark.io", SellerID = 44 },
                    new Customer { Id = 5, Name = "Audi AG", AccountOwner = "Angela.Roberts@CCSpark.io", SellerID = 48 },
                    new Customer { Id = 6, Name = "AOK Plus", AccountOwner = "Liu.Wei@CCSpark.io", SellerID = 5 },
                    new Customer { Id = 7, Name = "Telefonica", AccountOwner = "Michael.Brown@CCSpark.io", SellerID = 23 }
                );
                context.SaveChanges();
            }
        }
    }
}