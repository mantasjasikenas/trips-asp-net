# Trips agency

This is a simple ASP-NET application to manage trips agency database.

## Database structure

![F2 form](docs/images/db_lab2.png)

[LAB2 requirements](docs/pdf/LAB2.pdf)

## Delete cascade 
agents.id = 7

```mariadb
INSERT INTO `agents` (`id`, `first_name`, `last_name`, `phone`, `email`, `fk_travel_agency`)
VALUES (7, 'Easter', 'Lehrle', '+98 789 380 7191', 'elehrle6@netlog.com', 85);


INSERT INTO `orders` (`id`, `adults_count`, `children_count`, `final_price`, `status`, `luggage_size`, `fk_agent`,
                      `fk_customer`, `fk_trip`)
VALUES (5, 4, 0, '607.00', 1, 5, 7, 4, 27);


INSERT INTO `bills` (`nr`, `date`, `price`, `fk_order`)
VALUES (5, '2023-03-11', '607.00', 5);


INSERT INTO `payments` (`id`, `date`, `price`, `fk_bill`, `fk_customer`)
VALUES (5, '2023-03-11', '607.00', 5, 4);
```
